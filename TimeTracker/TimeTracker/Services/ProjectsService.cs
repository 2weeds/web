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

        public string Add(Project project, ClaimsPrincipal user)
        {
            string projectId = Add(project);
            string projectMemberId = projectMembersService.AddInitialUser(projectId, user);
            if (!string.IsNullOrEmpty(projectId) && !string.IsNullOrEmpty(projectMemberId))
            {
                return projectId;
            }
            return string.Empty;
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
            Project project = projectsRepository.Get(id);
            if (project != null)
            {
                project.ProjectMembers = projectMembersService.GetProjectMembersOfProject(id);
            }
            return project;
        }

        public Project Get(string projectId, ClaimsPrincipal user)
        {
            Project project = Get(projectId);
            if (project != null)
            {
                string currentUserId = userManager.GetUserId(user);
                project.ProjectMembers.Where(pm => pm.UserId == currentUserId)
                    .ToList().ForEach(pm => pm.IsCurrentUser = true);
            }
            return project;
            
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
                .Where(pm => pm.UserId != currentUserId)
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

        public string Update(Project model, ClaimsPrincipal principal)
        {
            if (projectMembersService.UpdateProjectMembersForProject(model.Id, model.ProjectMemberIds, principal))
            {
                return projectsRepository.Update(model);
            }
            return string.Empty;
        }

        public string Update(Project model)
        {
            throw new NotImplementedException();
        }
    }
}
