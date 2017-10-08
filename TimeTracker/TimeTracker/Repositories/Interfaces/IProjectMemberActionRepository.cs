using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Repositories.Interfaces
{
    public interface IProjectMemberActionRepository : IBaseRepository<ProjectMemberAction, string>
    {
        List<ProjectMemberAction> GetProjectMemberActions(string projectMemberId);
        bool UpdateProjectMemberActionsForMember(string projectMemberId, List<ProjectMemberAction> projectMemberActions);
        bool RemoveActionsOfProjectMember(string projectMemberId);
    }
}
