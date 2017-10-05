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
        private readonly IProjectMembersRepository projectMembersRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsService(
            IProjectsRepository projectsRepository, 
            IProjectMembersRepository projectMembersRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.projectsRepository = projectsRepository;
            this.projectMembersRepository = projectMembersRepository;
            this.userManager = userManager;
        }

        public string Add(Project model)
        {
            return projectsRepository.Add(model);
        }

        public bool CanUserRemoveMember(string userId, string projectId)
        {
            return projectMembersRepository.ProjectUserWithRoleExists(userId, projectId, 1);
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

        public ProjectCreateModel GetProjectCreateModel(ClaimsPrincipal user)
        {
            string currentUserId = userManager.GetUserId(user);
            return new ProjectCreateModel
            {
                UsernamesWithIds = userManager.Users
                    .Where(u => u.Id != currentUserId)
                    .Select(u => new ReactSelectListItem() { label = u.UserName, value = u.Id }).ToList()
            };
        }

        public bool Remove(string id)
        {
            return projectsRepository.Remove(id);
        }

        public string Update(Project model)
        {
            return projectsRepository.Update(model);
        }
    }
}
