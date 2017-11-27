using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Models;
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

        public List<ProjectMember> GetProjectMembersOfProject(string projectId)
        {
            return GetAll().Where(pm => pm.ProjectId == projectId).ToList();
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

        public bool RemoveMembersOfProject(string projectId)
        {
            List<ProjectMember> membersOfProjects = GetProjectMembersOfProject(projectId);
            dbContext.ProjectMembers.RemoveRange(membersOfProjects);
            try
            {
                dbContext.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }

        public List<ProjectMember> GetAllProjectMembersByUserId(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            return GetAll().Where(pm => pm.UserId == userId).ToList();
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

        public bool UpdateProjectMembersForProject(string projectId, List<ReactSelectListItem> projectMemberIds, string currentUserId)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                List<ProjectMember> previousProjectMembers = GetProjectMembersOfProject(projectId)
                    .Where(ppm => ppm.UserId != currentUserId).ToList();
                if (previousProjectMembers.Count > 0 && projectMemberIds == null)
                {
                    dbContext.RemoveRange(previousProjectMembers);
                } else if (projectMemberIds != null)
                {
                    List<ProjectMember> deletedProjectMembers = previousProjectMembers
                        .Where(ppm => !projectMemberIds.Any(pmi => pmi.value == ppm.Id)).ToList();
                    dbContext.RemoveRange(deletedProjectMembers);
                    List<ProjectMember> projectMembersToInsert = projectMemberIds
                        .Where(pmi => !deletedProjectMembers.Any(dpm => dpm.Id == pmi.value))
                        .Select(newId =>
                           new ProjectMember
                            {
                                MemberRole = 1,
                                ProjectId = projectId,
                                UserId = newId.value
                            }
                        ).ToList();
                    foreach (ProjectMember projectMember in projectMembersToInsert)
                    {
                        dbContext.ProjectMembers.Add(projectMember);
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
