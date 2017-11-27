using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {

        private readonly IProjectsService projectsService;
        private readonly IRegisteredActionsService registeredActionsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TrackingController(IProjectsService projectsService,
            IRegisteredActionsService registeredActionsService,
            UserManager<ApplicationUser> userManager)
        {
            this.projectsService = projectsService;
            this.registeredActionsService = registeredActionsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<Project> allProjects = projectsService.GetAll();
            List<ReactSelectListItem> projectsAsSelectListItems =
                allProjects.ToList().Select(project =>
                    new ReactSelectListItem
                    {
                        label = project.Title,
                        value = project.Id
                    }
            ).ToList();
            return View(projectsAsSelectListItems);
        }

        /*public JsonResult GetAvailableProjectMemberActions(string projectId)
        {
            if (projectId == null)
            {
                return new JsonResult(new { message = "ProjectIdMissing" });
            }
            string userId = userManager.GetUserId(HttpContext.User);
        }*/

    }
}