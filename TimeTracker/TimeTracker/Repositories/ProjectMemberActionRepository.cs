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

        public bool RemoveActionsOfProjectMember(string projectMemberId)
        {
            List<ProjectMemberAction> actionsOfProjectMember = GetProjectMemberActions(projectMemberId);
            dbContext.ProjectMemberActions.RemoveRange(actionsOfProjectMember);
            try
            {
                dbContext.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
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

        public bool UpdateProjectMemberActionsForMember(string projectMemberId, List<ProjectMemberAction> projectMemberActions)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                List<ProjectMemberAction> previousActions =
                    GetProjectMemberActions(projectMemberId);
                if (previousActions.Count > 0 && projectMemberActions.Count == 0)
                {
                    dbContext.RemoveRange(previousActions);
                } else if (projectMemberActions.Count > 0)
                {
                    List<ProjectMemberAction> deletedProjectActions = previousActions
                        .Where(pa => !projectMemberActions.Any(pma => pma.Id == pa.Id)).ToList();
                    dbContext.RemoveRange(deletedProjectActions);
                    projectMemberActions.RemoveAll(
                        x => previousActions.Any(pa => pa.Description == x.Description) || 
                        deletedProjectActions.Any(dpa => dpa.Description == x.Description));
                    foreach (ProjectMemberAction action in projectMemberActions)
                    {
                        action.Id = null;
                        dbContext.ProjectMemberActions.Add(action);
                    }
                }
                try
                {
                    dbContext.SaveChanges();
                    transaction.Commit();
                    return true;
                } catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
