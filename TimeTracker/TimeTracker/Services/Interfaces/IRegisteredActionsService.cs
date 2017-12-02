using System.Collections.Generic;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Services.Interfaces
{
    public interface IRegisteredActionsService : IBaseService<RegisteredAction, string>
    {
        List<RegisteredAction> GetRegisteredProjectMemberActions(string projectMemberId);
        List<RegisteredAction> GetAllProjectRegisteredActions(string projectId);
        bool UpdateRegisteredActions(List<RegisteredAction> registeredActions, string projectmemberId);
        bool UpdateProjectActions(List<RegisteredAction> projectActions, string projectId);
    }
}