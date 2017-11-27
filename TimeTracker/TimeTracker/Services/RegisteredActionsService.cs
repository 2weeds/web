using System.Collections.Generic;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Services
{
    public class RegisteredActionsService : IRegisteredActionsService
    {

        private readonly IRegisteredActionRepository registeredActionRepository;

        public RegisteredActionsService(
            IRegisteredActionRepository registeredActionRepository)
        {
            this.registeredActionRepository = registeredActionRepository;
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
    }
}