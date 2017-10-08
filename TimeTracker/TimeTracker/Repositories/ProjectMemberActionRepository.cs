using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;

namespace TimeTracker.Repositories
{
    public class ProjectMemberActionRepository : IProjectMemberActionRepository
    {

        private readonly ApplicationDbContext dbContext;

        public ProjectMemberActionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Add(ProjectMemberAction model)
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
            return dbContext.ProjectMemberActions.Any(p => p.Id == id);
        }

        public ProjectMemberAction Get(string id)
        {
            if (id == null)
            {
                return null;
            }
            return dbContext.ProjectMemberActions
                .SingleOrDefault(pm => pm.Id == id);
        }

        public IEnumerable<ProjectMemberAction> GetAll()
        {
            return dbContext.ProjectMemberActions.ToList();
        }

        public List<ProjectMemberAction> GetProjectMemberActions(string projectMemberId)
        {
            return GetAll().Where(pma => pma.ProjectMemberId == projectMemberId).ToList();
        }

        public bool Remove(string id)
        {
            var projectMemberAction = dbContext.ProjectMemberActions.SingleOrDefault(pm => pm.Id == id);
            dbContext.ProjectMemberActions.Remove(projectMemberAction);
            dbContext.SaveChanges();
            return projectMemberAction.Id == null;
        }

        public string Update(ProjectMemberAction model)
        {
            try
            {
                dbContext.Update(model);
                dbContext.SaveChanges();
                return model.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
