using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Controllers
{
    public class TimersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Timers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Timers.ToListAsync());
        }

        // GET: Timers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timer = await _context.Timers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (timer == null)
            {
                return NotFound();
            }

            return View(timer);
        }

        // GET: Timers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,ProjectId,DateFrom,DateTo")] Timer timer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(timer);
        }

        // GET: Timers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timer = await _context.Timers.SingleOrDefaultAsync(m => m.Id == id);
            if (timer == null)
            {
                return NotFound();
            }
            return View(timer);
        }

        // POST: Timers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Description,ProjectId,DateFrom,DateTo")] Timer timer)
        {
            if (id != timer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimerExists(timer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(timer);
        }

        // GET: Timers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timer = await _context.Timers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (timer == null)
            {
                return NotFound();
            }

            return View(timer);
        }

        // POST: Timers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var timer = await _context.Timers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Timers.Remove(timer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TimerExists(string id)
        {
            return _context.Timers.Any(e => e.Id == id);
        }
    }
}
