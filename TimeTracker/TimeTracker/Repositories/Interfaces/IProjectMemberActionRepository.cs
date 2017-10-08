using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Repositories.Interfaces
{
    interface IProjectMemberActionRepository : IBaseRepository<ProjectMemberAction, string>
    {
        List<ProjectMemberAction> GetProjectMemberActions(string projectMemberId);
    }
}
