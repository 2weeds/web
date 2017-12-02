using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models.ProjectModels
{
    public class ProjectAction : IComparable<ProjectAction>
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string ProjectId { get; set; }

        public int CompareTo(ProjectAction other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var descriptionComparison = string.Compare(Description, other.Description, StringComparison.Ordinal);
            if (descriptionComparison != 0) return descriptionComparison;
            return string.Compare(ProjectId, other.ProjectId, StringComparison.Ordinal);
        }
    }
}
