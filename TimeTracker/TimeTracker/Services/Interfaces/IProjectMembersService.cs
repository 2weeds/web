using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Services.Interfaces
{
    public interface IProjectMembersService: IBaseService<ProjectMember, string>
    {
        bool ProjectUserWithRoleExists(string userId, string projectId, int role);
        bool UpdateProjectMembersForProject(string projectId, List<ReactSelectListItem> projectMemberIds);
        List<ProjectMember> GetProjectMembersOfProject(string projectId);
        bool RemoveMembersOfProject(string projectId);
    }
}
