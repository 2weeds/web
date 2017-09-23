using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Services.Interfaces;
using TimeTracker.Repositories.Interfacies;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;

namespace TimeTracker.Services
{
    public class ProjectsService : IProjectsService
    {

        private readonly IProjectsRepository projectsRepository;
        private readonly IProjectMembersRepository projectMembersRepository;

        public ProjectsService(IProjectsRepository projectsRepository, IProjectMembersRepository projectMembersRepository)
        {
            this.projectsRepository = projectsRepository;
            this.projectMembersRepository = projectMembersRepository;
        }

        public string Add(Project model)
        {
            return projectsRepository.Add(model);
        }

        public bool CanUserRemoveMember(string userId, string projectId)
        {
            return projectMembersRepository.ProjectUserWithRoleExists(userId, projectId, 1);
        }

        public bool Exists(string id)
        {
            return projectsRepository.Exists(id);
        }

        public Project Get(string id)
        {
            return projectsRepository.Get(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return projectsRepository.GetAll();
        }

        public bool Remove(string id)
        {
            return projectsRepository.Remove(id);
        }

        public string Update(Project model)
        {
            return projectsRepository.Update(model);
        }
    }
}
