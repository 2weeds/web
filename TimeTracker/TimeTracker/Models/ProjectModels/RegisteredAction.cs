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
        
        public int Duration { get; set; }
        
        public string ProjectMemberId { get; set; }
        
        public string ProjectActionId { get; set; }
        
    }
}