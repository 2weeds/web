using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Repositories.Interfacies;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectsService projectsService;

        public HomeController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
