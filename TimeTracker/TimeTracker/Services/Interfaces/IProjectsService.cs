using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Models.ProjectModels;

namespace TimeTracker.Services.Interfaces
{
    public interface IProjectsService : IBaseService<Project, string>
    {
        bool CanUserRemoveMember(string userId, string projectId);
        List<ReactSelectListItem> GetProjectCreateModel(string projectId, ClaimsPrincipal user);
    }
}
