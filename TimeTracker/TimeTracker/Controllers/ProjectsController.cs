using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfacies;
using TimeTracker.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace TimeTracker.Controllers
{
    public class ProjectsController : Controller
    {

        private readonly IProjectsService projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        // GET: Projects
        public IActionResult Index()
        {
            return View(projectsService.GetAll());
        }

        // GET: Projects/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectsService.Get(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View(projectsService.GetProjectCreateModel(HttpContext.User));
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title")] Project project)
        {
            if (ModelState.IsValid)
            {
                projectsService.Add(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectsService.Get(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,Title")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string maybeId = projectsService.Update(project);
                if (maybeId == null)
                {
                    if (!projectsService.Exists(project.Id))
                    {
                        return NotFound();
                    } else
                    {
                        throw new Exception();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = projectsService.Get(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            projectsService.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
