using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Repositories.Interfaces
{
    public interface IProjectMembersRepository : IBaseRepository<ProjectMember, string>
    {
        bool ProjectUserWithRoleExists(string userId, string projectId, int role);
        bool UpdateProjectMembersForProject(string projectId, List<ReactSelectListItem> projectMemberIds, string currentUserId);
        List<ProjectMember> GetProjectMembersOfProject(string projectId);
        bool RemoveMembersOfProject(string projectId);
    }
}
