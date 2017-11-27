using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Data;
using TimeTracker.Models.ProjectModels;
using TimeTracker.Repositories.Interfaces;

namespace TimeTracker.Repositories
{
    public class RegisteredActionsRepository : IRegisteredActionRepository
    {

        private readonly ApplicationDbContext context;

        public RegisteredActionsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public IEnumerable<RegisteredAction> GetAll()
        {
            return context.RegisteredActions.ToList();
        }

        public RegisteredAction Get(string id)
        {
            if (id == null)
            {
                return null;
            }
            return context.RegisteredActions
                .SingleOrDefault(m => m.Id == id);
        }

        public string Add(RegisteredAction model)
        {
            if (model.Id != null)
            {
                return null;
            }
            context.Add(model);
            context.SaveChanges();
            return model.Id;
        }

        public bool Remove(string id)
        {
            var action = context.RegisteredActions.SingleOrDefault(m => m.Id == id);
            context.RegisteredActions.Remove(action);
            context.SaveChanges();
            return action.Id == null;
        }

        public bool Exists(string id)
        {
            return context.RegisteredActions.Any(p => p.Id == id);
        }

        public string Update(RegisteredAction model)
        {
            try
            {
                RegisteredAction action = Get(model.Id);
                context.RegisteredActions.Attach(action);
                action.StartTime = model.StartTime;
                action.EndTime = model.EndTime;
                action.ProjectMemberActionId = model.ProjectMemberActionId;
                action.ProjectMemberId = model.ProjectMemberId;
                context.SaveChanges();
                return model.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}