using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.TeamModels;

namespace TimeTracker.Repositories.Interfacies
{
    public interface ITeamRepository
    {
        List<Team> GetAll();


    }
}
