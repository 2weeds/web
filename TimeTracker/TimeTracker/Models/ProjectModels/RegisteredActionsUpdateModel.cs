using System;
using System.Collections.Generic;

namespace TimeTracker.Models.ProjectModels
{
    public class RegisteredActionsUpdateModel
    {
        public List<RegisteredAction> RegisteredActions { get; set; }
        public String ProjectMemberId { get; set; }
        public bool IsProjectManager { get; set; }
        public string ProjectId { get; set; }
    }
}