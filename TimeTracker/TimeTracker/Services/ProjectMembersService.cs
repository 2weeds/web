using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProjectMembersService(IProjectMembersRepository projectMembersRepository,
            IProjectMemberActionRepository projectMemberActionsRepository)
        {
            this.projectMembersRepository = projectMembersRepository;
            this.projectMemberActionsRepository = projectMemberActionsRepository;
        }

        public string Add(ProjectMember model)
        {
            return projectMembersRepository.Add(model);
        }

        public bool Exists(string id)
        {
            return projectMembersRepository.Exists(id);
        }

        public ProjectMember Get(string id)
        {
            return projectMembersRepository.Get(id);
        }

        public IEnumerable<ProjectMember> GetAll()
        {
            return projectMembersRepository.GetAll();
        }

        public List<ProjectMember> GetProjectMembersOfProject(string projectId)
        {
            return projectMembersRepository.GetProjectMembersOfProject(projectId);
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
            return projectMembersRepository.Update(model);
        }

        public bool UpdateProjectMembersForProject(string projectId, List<ReactSelectListItem> projectMemberIds)
        {
            return projectMembersRepository.UpdateProjectMembersForProject(projectId, projectMemberIds);
        }
    }
}
