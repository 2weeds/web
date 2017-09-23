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
            return dbContext.ProjectMembers.Any(p => p.Id == id);
        }

        public ProjectMember Get(string id)
        {
            if (id == null)
            {
                return null;
            }
            return dbContext.ProjectMembers
                .SingleOrDefault(pm => pm.Id == id);
        }

        public IEnumerable<ProjectMember> GetAll()
        {
            return dbContext.ProjectMembers.ToList();
        }

        public bool ProjectUserWithRoleExists(string userId, string projectId, int role)
        {
            return dbContext.ProjectMembers.Any(pm => pm.UserId == userId && pm.ProjectId == projectId && pm.MemberRole == role);
        }

        public bool Remove(string id)
        {
            var projectMember = dbContext.ProjectMembers.SingleOrDefault(pm => pm.Id == id);
            dbContext.ProjectMembers.Remove(projectMember);
            dbContext.SaveChanges();
            return projectMember.Id == null;
        }

        public string Update(ProjectMember model)
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
