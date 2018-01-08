using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Repositories.Interfaces
{
    public interface IProjectActionRepository : IBaseRepository<ProjectAction, string>
    {
        bool UpdateProjectActions(List<ProjectAction> projectActions, string projectId);
        List<ProjectAction> GetProjectActions(string projectId);
    }
}
