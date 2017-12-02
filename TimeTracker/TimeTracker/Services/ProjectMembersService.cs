using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Services
{
    public class ProjectMembersService : IProjectMembersService
    {

        private readonly IProjectMembersRepository projectMembersRepository;
        private readonly IProjectActionRepository _projectActionsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectMembersService(IProjectMembersRepository projectMembersRepository,
            IProjectActionRepository projectActionsRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.projectMembersRepository = projectMembersRepository;
            this._projectActionsRepository = projectActionsRepository;
            this.userManager = userManager;
        }

        public string Add(ProjectMember model)
        {
            return projectMembersRepository.Add(model);
        }

        public string AddInitialUser(string projectId, ClaimsPrincipal user)
        {
            ProjectMember projectMember = new ProjectMember
            {
                MemberRole = 1,
                ProjectId = projectId,
                UserId = userManager.GetUserId(user)
            };
            return Add(projectMember);
        }

        public List<ProjectMember> GetAllProjectMembersByUserId(string userId)
        {
            return projectMembersRepository.GetAllProjectMembersByUserId(userId);
        }

        public bool Exists(string id)
        {
            return projectMembersRepository.Exists(id);
        }

        public ProjectMember Get(string id)
        {
            ProjectMember projectMember = projectMembersRepository.Get(id);
            return projectMember;
        }

        public IEnumerable<ProjectMember> GetAll()
        {
            return projectMembersRepository.GetAll();
        }

        public List<ProjectMember> GetProjectMembersOfProject(string projectId, string currentUserId)
        {
            List<ProjectMember> projectMembers = projectMembersRepository.GetProjectMembersOfProject(projectId);
            projectMembers.ForEach(pm =>
            {
                if (pm.UserId == currentUserId)
                {
                    pm.IsCurrentUser = true;
                }
            });
            return projectMembers;
        }

        public bool ProjectUserWithRoleExists(string userId, string projectId, int role)
        {
            return projectMembersRepository.ProjectUserWithRoleExists(userId, projectId, role);
        }

        public bool Remove(string id)
        {
            return projectMembersRepository.Remove(id);
        }

        public bool RemoveMembersOfProject(string projectId)
        {
            return projectMembersRepository.RemoveMembersOfProject(projectId);
        }

        public string Update(ProjectMember model)
        {
            string updatedProjectId = projectMembersRepository.Update(model);
            return projectMembersRepository.Update(model);
        }

        public bool UpdateProjectMembersForProject(string projectId, Project project, ClaimsPrincipal claimsPrincipal)
        {
            string currentUserId = userManager.GetUserId(claimsPrincipal);
            return projectMembersRepository.UpdateProjectMembersForProject(projectId, project.ProjectMemberIds, currentUserId);
        }
    }
}
