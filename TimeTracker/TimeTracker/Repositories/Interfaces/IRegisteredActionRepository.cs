using System.Collections.Generic;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Repositories.Interfaces
{
    public interface IRegisteredActionRepository : IBaseRepository<RegisteredAction, string>
    {
        List<RegisteredAction> GetRegisteredProjectMemberActions(string projectMemberId);
        List<RegisteredAction> GetProjectRegisteredActions(List<string> projectMemberIds);
    }
}