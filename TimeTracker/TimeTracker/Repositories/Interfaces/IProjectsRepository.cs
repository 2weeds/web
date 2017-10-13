using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;

namespace TimeTracker.Repositories.Interfacies
{
    public interface IProjectsRepository : IBaseRepository<Project, string>
    {
    }
}
