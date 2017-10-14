using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Services.Interfaces
{
    public interface IProjectMembersService: IBaseService<ProjectMember, string>
    {
        bool ProjectUserWithRoleExists(string userId, string projectId, int role);
        bool UpdateProjectMembersForProject(string projectId, Project project, ClaimsPrincipal user);
        List<ProjectMember> GetProjectMembersOfProject(string projectId, string currentUserId);
        bool RemoveMembersOfProject(string projectId);
        string AddInitialUser(string projectId, ClaimsPrincipal user);
    }
}
