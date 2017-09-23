using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;

namespace TimeTracker.Repositories
{
    public class ProjectMembersRepository : IProjectMembersRepository
    {

        private readonly ApplicationDbContext dbContext;

        public ProjectMembersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Add(ProjectMember model)
        {
            if (model.Id != null)
            {
                return null;
            }
            dbContext.Add(model);
            dbContext.SaveChanges();
            return model.Id;
        }

        public bool Exists(string id)
        {
            return false;
            //dbContext.ProjectMembers.Any(p => p.Id == id);
        }

        public ProjectMember Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectMember> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }

        public string Update(ProjectMember model)
        {
            throw new NotImplementedException();
        }
    }
}
