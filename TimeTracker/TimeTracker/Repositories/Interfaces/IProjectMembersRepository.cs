using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Repositories.Interfaces
{
    public interface IProjectMembersRepository : IBaseRepository<ProjectMember, string>
    {
        bool ProjectUserWithRoleExists(string userId, string projectId, int role);
    }
}
