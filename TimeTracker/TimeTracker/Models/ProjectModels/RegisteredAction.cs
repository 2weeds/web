using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Models.ProjectModels
{
    public class RegisteredAction
    {
        [Key]
        public string Id { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        [NotMapped]
        public string ProjectMemberId { get; set; }
        
        public string ProjectMemberActionId { get; set; }
        
    }
}