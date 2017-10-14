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
        private readonly IProjectMemberActionRepository projectMemberActionsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectMembersService(IProjectMembersRepository projectMembersRepository,
            IProjectMemberActionRepository projectMemberActionsRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.projectMembersRepository = projectMembersRepository;
            this.projectMemberActionsRepository = projectMemberActionsRepository;
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

        public bool Exists(string id)
        {
            return projectMembersRepository.Exists(id);
        }

        public ProjectMember Get(string id)
        {
            ProjectMember projectMember = projectMembersRepository.Get(id);
            if (projectMember != null)
            {
                projectMember.ProjectMemberActions =
                    projectMemberActionsRepository.GetProjectMemberActions(id);
            }
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
                List<ProjectMemberAction> projectMemberActions = projectMemberActionsRepository.GetProjectMemberActions(pm.Id);
                projectMemberActions.Sort((first, second) => first.Description.CompareTo(second.Description));
                pm.ProjectMemberActions = projectMemberActions;
            });
            return projectMembers;
        }

        public bool ProjectUserWithRoleExists(string userId, string projectId, int role)
        {
            return projectMembersRepository.ProjectUserWithRoleExists(userId, projectId, role);
        }

        public bool Remove(string id)
        {
            bool projectMemberRemovalSuccessful = projectMemberActionsRepository.RemoveActionsOfProjectMember(id);
            if (projectMemberRemovalSuccessful)
            {
                return projectMembersRepository.Remove(id);
            }
            return false;
        }

        public bool RemoveMembersOfProject(string projectId)
        {
            return projectMembersRepository.RemoveMembersOfProject(projectId);
        }

        public string Update(ProjectMember model)
        {
            string updatedProjectId = projectMembersRepository.Update(model);
            if (updatedProjectId != null)
            {
                return projectMemberActionsRepository.UpdateProjectMemberActionsForMember(updatedProjectId, model.ProjectMemberActions) ? updatedProjectId : string.Empty;
            }
            return projectMembersRepository.Update(model);
        }

        public bool UpdateProjectMembersForProject(string projectId, Project project, ClaimsPrincipal claimsPrincipal)
        {
            string currentUserId = userManager.GetUserId(claimsPrincipal);
            bool projectMembersUpdated = projectMembersRepository.UpdateProjectMembersForProject(projectId, project.ProjectMemberIds, currentUserId);
            //project.ProjectMembers = GetProjectMembersOfProject(projectId, userManager.GetUserId(claimsPrincipal));
            if (projectMembersUpdated)
            {
                ProjectMember currentUserAsMember = project.ProjectMembers.FirstOrDefault(pm => pm.IsCurrentUser);
                if (currentUserAsMember != null)
                {
                    bool success = projectMemberActionsRepository.UpdateProjectMemberActionsForMember(currentUserAsMember.Id, currentUserAsMember.ProjectMemberActions);
                    if (!success)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
