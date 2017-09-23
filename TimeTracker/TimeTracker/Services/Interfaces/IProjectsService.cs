using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Services.Interfaces
{
    public interface IProjectsService : IBaseService<Project, string>
    {
        bool CanUserRemoveMember(string userId, string projectId);
    }
}
