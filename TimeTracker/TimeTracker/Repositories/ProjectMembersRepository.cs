using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Remotion.Linq.Parsing;
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

        private bool PerformDuplicateUserCleanup(string projectId)
        {
            try
            {
                List<ProjectMember> projectMembersOfProject = GetProjectMembersOfProject(projectId);
                IEnumerable<string> allProjectMemberIds = projectMembersOfProject.Select(x => x.UserId);
                IEnumerable<string> distinctProjectMemberIds = allProjectMemberIds.Distinct();
                foreach (string distinctProjectMemberId in distinctProjectMemberIds)
                {
                    int eachIdCount = allProjectMemberIds.Count(x => x == distinctProjectMemberId);
                    for (int i = 0; i < eachIdCount - 1; i++)
                    {
                        ProjectMember projectMember =
                            projectMembersOfProject.First(x => x.UserId == distinctProjectMemberId);
                        Remove(projectMember.Id);
                    }
                }                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProjectMembersForProject(string projectId, List<ReactSelectListItem> projectMemberIds, string currentUserId)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                //PerformDuplicateUserCleanup(projectId);
                List<ProjectMember> previousProjectMembers = GetProjectMembersOfProject(projectId);
                List<string> deletedUserIds = new List<string>();
                foreach (ProjectMember previousProjectMember in previousProjectMembers)
                {
                    if (previousProjectMember.UserId != currentUserId)
                    {
                        if (!projectMemberIds.Any(x => x.value == previousProjectMember.UserId))
                        {
                            deletedUserIds.Add(previousProjectMember.UserId);
                        }
                        else
                        {
                            projectMemberIds.RemoveAll(x => x.value == previousProjectMember.UserId);
                        }
                    }
                }
                foreach (string deletedUserId in deletedUserIds)
                {
                    IEnumerable<ProjectMember> deletedMembers =
                        previousProjectMembers.Where(x => x.UserId == deletedUserId).ToList();
                    foreach (ProjectMember projectMember in deletedMembers)
                    {
                        Remove(projectMember.Id);
                    }
                }
                List<ProjectMember> projectMembersToInsert =
                    projectMemberIds.Select(x => new ProjectMember
                    {
                        UserId = x.value,
                        ProjectId = projectId,
                        MemberRole = 0
                    }).ToList();
                foreach (ProjectMember projectMember in projectMembersToInsert)
                {
                    Add(projectMember);
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
