using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {

        private readonly IProjectsService projectsService;
        private readonly IRegisteredActionsService registeredActionsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProjectMembersService projectMembersService;
        private readonly IProjectActionRepository projectActionsRepository;

        public TrackingController(IProjectsService projectsService,
            IRegisteredActionsService registeredActionsService,
            UserManager<ApplicationUser> userManager,
            IProjectMembersService projectMembersService,
            IProjectActionRepository projectActionsRepository)
        {
            this.projectsService = projectsService;
            this.registeredActionsService = registeredActionsService;
            this.userManager = userManager;
            this.projectMembersService = projectMembersService;
            this.projectActionsRepository = projectActionsRepository;
        }

        public IActionResult Index()
        {
            string currentUserId =
                userManager.GetUserId(HttpContext.User);
            List<ReactSelectListItem> allProjects = projectsService.GetUserProjects(currentUserId);
            List<ReactProjectSelectListItem> projectsAsSelectListItems =
                allProjects.ToList().Select(project =>
                    new ReactProjectSelectListItem
                    {
                        label = project.label,
                        value = project.value,
                        projectMemberId =
                            projectMembersService.GetProjectMembersOfProject(project.value, currentUserId)
                                .First(x => x.IsCurrentUser).Id,
                        isProjectManager =
                            projectMembersService.GetProjectMembersOfProject(project.value, currentUserId)
                                .First(x => x.IsCurrentUser).MemberRole == 1
                    }
                ).ToList();
            return View(projectsAsSelectListItems);
        }

        public JsonResult RegisterTime(string projectMemberActionId, string projectMemberId, int duration)
        {
            if (projectMemberActionId == null || duration <= 0)
            {
                return new JsonResult(new {message = "MissingParameters"});
            }
            DateTime now = DateTime.Now;
            ProjectAction projectMemberAction = projectActionsRepository.Get(projectMemberActionId);
            RegisteredAction registeredAction = new RegisteredAction
            {
                StartTime = now,
                Duration = duration,
                ProjectMemberId = projectMemberId,    
                ProjectActionId =  projectMemberActionId
            };
            string result = registeredActionsService.Add(registeredAction);
            return new JsonResult(new {result = result});
        }

        public JsonResult GetProjectMemberRegisteredTimes(string projectMemberId)
        {
            if (projectMemberId == null)
            {
                return new JsonResult(new {message = "MissingParameters"});
            }
            List<RegisteredAction> registeredActions =
                registeredActionsService.GetRegisteredProjectMemberActions(projectMemberId);
            return new JsonResult(new {result = registeredActions});
        }

        public JsonResult UpdateRegisteredTimes([FromBody]RegisteredActionsUpdateModel model)
        {
            if (model == null || model.RegisteredActions == null) 
            {
                return new JsonResult(new {message = "MissingParameters"});
            }
            bool updateResult = 
                registeredActionsService.UpdateRegisteredActions(
                    model.RegisteredActions, model.ProjectMemberId);
            return new JsonResult(new {message = updateResult ? "Success" : "Fail"});
        }

    }
}