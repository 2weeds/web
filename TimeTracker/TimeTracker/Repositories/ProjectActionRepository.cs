using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;

namespace TimeTracker.Repositories
{
    public class ProjectActionRepository : IProjectActionRepository
    {

        private readonly ApplicationDbContext dbContext;

        public ProjectActionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Add(ProjectAction model)
        {
            if(!string.IsNullOrEmpty(model.Id))
            {
                return null;
            }
            model.Id = null;
            try
            {
                dbContext.ProjectActions.Add(model);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                e = e;
            }
            return model.Id;
        }

        public bool Exists(string id)
        {
            return dbContext.ProjectActions.Any(p => p.Id == id);
        }

        public ProjectAction Get(string id)
        {
            if (id == null)
            {
                return null;
            }
            return dbContext.ProjectActions
                .SingleOrDefault(pm => pm.Id == id);
        }

        public IEnumerable<ProjectAction> GetAll()
        {
            return dbContext.ProjectActions.ToList();
        }

        public List<ProjectAction> GetProjectMemberActions(string projectMemberId)
        {
            return new List<ProjectAction>();
        }

        public bool Remove(string id)
        {
            var projectMemberAction = dbContext.ProjectActions.SingleOrDefault(pm => pm.Id == id);
            dbContext.ProjectActions.Remove(projectMemberAction);
            dbContext.SaveChanges();
            return projectMemberAction.Id == null;
        }

        public string Update(ProjectAction model)
        {
            try
            {
                ProjectAction projectAction = Get(model.Id);
                dbContext.ProjectActions.Attach(projectAction);
                projectAction.Id = model.Id;
                projectAction.ProjectId = model.ProjectId;
                projectAction.Description = model.Description;
                dbContext.SaveChanges();
                return model.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public bool UpdateProjectActions(List<ProjectAction> projectActions, string projectId)
        {
            IEnumerable<ProjectAction> previousProjectActions = GetAll()
                .Where(x => x.ProjectId == projectId);
            IEnumerable<ProjectAction> deletedProjectActions = previousProjectActions
                .Where(ppa => !projectActions.Any(pa => ppa.Id == pa.Id));
            foreach (ProjectAction deletedProjectAction in deletedProjectActions)
            {
                Remove(deletedProjectAction.Id);
            }
            foreach (ProjectAction projectAction in projectActions)
            {
                if (string.IsNullOrEmpty(projectAction.Id))
                {
                    Add(projectAction);
                }
                else
                {
                    ProjectAction actionFromDb =
                        previousProjectActions.FirstOrDefault(x => x.Id == projectAction.Id);
                    if (actionFromDb != null && actionFromDb.CompareTo(projectAction) != 0)
                    {
                        Update(projectAction);
                    }
                }
            }
            return true;
        }

        public List<ProjectAction> GetProjectActions(string projectId)
        {
            return GetAll().Where(x => x.ProjectId == projectId).ToList();
        }
    }
}
