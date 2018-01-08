using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Services
{
    public class RegisteredActionsService : IRegisteredActionsService
    {

        private readonly IRegisteredActionRepository registeredActionRepository;
        private readonly IProjectMembersRepository projectMembersRepository;

        public RegisteredActionsService(
            IRegisteredActionRepository registeredActionRepository,
            IProjectMembersRepository projectMembersRepository)
        {
            this.registeredActionRepository = registeredActionRepository;
            this.projectMembersRepository = projectMembersRepository;
        }
        
        public IEnumerable<RegisteredAction> GetAll()
        {
            return  registeredActionRepository.GetAll();
        }

        public RegisteredAction Get(string id)
        {
            return registeredActionRepository.Get(id);
        }

        public string Add(RegisteredAction model)
        {
            return registeredActionRepository.Add(model);
        }

        public bool Remove(string id)
        {
            return registeredActionRepository.Remove(id);
        }

        public bool Exists(string id)
        {
            return registeredActionRepository.Exists(id);
        }

        public string Update(RegisteredAction model)
        {
            return registeredActionRepository.Update(model);
        }

        public List<RegisteredAction> GetRegisteredProjectMemberActions(string projectMemberId)
        {
            return registeredActionRepository.GetRegisteredProjectMemberActions(projectMemberId);
        }

        public List<RegisteredAction> GetAllProjectRegisteredActions(string projectId)
        {
            List<string> projectMembersIds = projectMembersRepository
                .GetProjectMembersOfProject(projectId).Select(x => x.Id).Distinct().ToList();
            return registeredActionRepository.GetProjectRegisteredActions(projectMembersIds);
        }

        public bool UpdateRegisteredActions(List<RegisteredAction> registeredActions, string projectMemberId)
        {
            try
            {
                IEnumerable<RegisteredAction> allRegisteredActions = registeredActionRepository.GetAll()
                    .Where(x => x.ProjectMemberId == projectMemberId);
                List<RegisteredAction> deletedProjectMembers = allRegisteredActions
                    .Where(ppm => !registeredActions.Any(pmi => pmi.Id == ppm.Id)).ToList();
                foreach (var registeredAction in deletedProjectMembers)
                {
                    registeredActionRepository.Remove(registeredAction.Id);
                }
                foreach (RegisteredAction registeredAction in registeredActions)
                {
                    RegisteredAction actionFromDatabase = registeredActionRepository.Get(registeredAction.Id);
                    if (!registeredAction.Equals(actionFromDatabase))
                    {
                        registeredActionRepository.Update(registeredAction);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateProjectActions(List<RegisteredAction> projectActions, string projectId)
        {
            try
            {
                List<string> projectMemberIds = projectMembersRepository
                    .GetProjectMembersOfProject(projectId)
                    .Select(x => x.Id).Distinct().ToList();
                List<RegisteredAction> projectRegisteredActions =
                    registeredActionRepository.GetProjectRegisteredActions(projectMemberIds);
                List<RegisteredAction> deletedActions =
                    projectRegisteredActions.Where(ppa =>
                        !projectActions.Any(pa => pa.Id == ppa.Id)).ToList();
                foreach (RegisteredAction registeredAction in deletedActions)
                {
                    registeredActionRepository.Remove(registeredAction.Id);
                }
                foreach (RegisteredAction registeredAction in projectActions)
                {
                    if (string.IsNullOrEmpty(registeredAction.Id))
                    {
                        Add(registeredAction);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}