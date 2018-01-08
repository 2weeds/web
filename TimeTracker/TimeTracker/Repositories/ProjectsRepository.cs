using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfacies;

namespace TimeTracker.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {

        private readonly ApplicationDbContext dbContext;

        public ProjectsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Add(Project model)
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
            return dbContext.Projects.Any(p => p.Id == id);
        }

        public Project Get(string id)
        {
            if (id == null)
            {
                return null;
            }

            return dbContext.Projects
                .SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Project> GetAll()
        {
            return dbContext.Projects.ToList();
        }

        public bool Remove(string id)
        {
            var project = dbContext.Projects.SingleOrDefault(m => m.Id == id);
            dbContext.Projects.Remove(project);
            dbContext.SaveChanges();
            return project.Id == null;
        }

        public string Update(Project model)
        {
            try
            {
                Project project = Get(model.Id);
                dbContext.Projects.Attach(project);
                project.Title = model.Title;
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
