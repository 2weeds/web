using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Services.Interfaces;
using TimeTracker.Repositories.Interfacies;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;
using TimeTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace TimeTracker.Services
{
    public class ProjectsService : IProjectsService
    {

        private readonly IProjectsRepository projectsRepository;
        private readonly IProjectMembersService projectMembersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsService(
            IProjectsRepository projectsRepository,
            IProjectMembersService projectMembersService,
            UserManager<ApplicationUser> userManager)
        {
            this.projectsRepository = projectsRepository;
            this.projectMembersService = projectMembersService;
            this.userManager = userManager;
        }

        public string Add(Project model)
        {
            return projectsRepository.Add(model);
        }

        public bool CanUserRemoveMember(string userId, string projectId)
        {
            return projectMembersService.ProjectUserWithRoleExists(userId, projectId, 1);
        }

        public bool Exists(string id)
        {
            return projectsRepository.Exists(id);
        }

        public Project Get(string id)
        {
            return projectsRepository.Get(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return projectsRepository.GetAll();
        }

        public List<ReactSelectListItem> GetProjectCreateModel(string projectId, ClaimsPrincipal user)
        {
            string currentUserId = userManager.GetUserId(user);
            List<ReactSelectListItem> allUserSelectItems = userManager.Users
                .Where(u => u.Id != currentUserId)
                .Select(u => new ReactSelectListItem() { label = u.UserName, value = u.Id }).ToList();
            if (projectId == null)
            {
                return allUserSelectItems;
            }
            List<ReactSelectListItem> projectMembers =
                projectMembersService.GetProjectMembersOfProject(projectId)
                .Select(pm => new ReactSelectListItem
                {
                    label = allUserSelectItems.FirstOrDefault(pcm => pcm.value == pm.UserId).label,
                    value = pm.UserId
                }).ToList();
            return projectMembers;
        }

        public bool Remove(string id)
        {
            bool projectMembersRemovalSuccessful = projectMembersService.RemoveMembersOfProject(id);
            if (projectMembersRemovalSuccessful)
            {
                return projectsRepository.Remove(id);
            }
            return false;
        }

        public string Update(Project model)
        {
            if (projectMembersService.UpdateProjectMembersForProject(model.Id, model.ProjectMemberIds))
            {
                return projectsRepository.Update(model);
            }
            return string.Empty;
        }
    }
}
