using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Models.ProjectModels
{
    public class RegisteredAction : IComparable<RegisteredAction>
    {
        [Key]
        public string Id { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public int Duration { get; set; }
        
        public string ProjectMemberId { get; set; }
        
        public string ProjectActionId { get; set; }

        public int CompareTo(RegisteredAction other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = string.Compare(Id, other.Id, StringComparison.Ordinal);
            if (idComparison != 0) return idComparison;
            var startTimeComparison = StartTime.CompareTo(other.StartTime);
            if (startTimeComparison != 0) return startTimeComparison;
            var durationComparison = Duration.CompareTo(other.Duration);
            if (durationComparison != 0) return durationComparison;
            var projectMemberIdComparison = string.Compare(ProjectMemberId, other.ProjectMemberId, StringComparison.Ordinal);
            if (projectMemberIdComparison != 0) return projectMemberIdComparison;
            return string.Compare(ProjectActionId, other.ProjectActionId, StringComparison.Ordinal);
        }
    }
}