using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTracker.Models.ProjectModels
{
    public class Project
    {

        [Key]
        public string Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [NotMapped]
        public List<ReactSelectListItem> ProjectMemberIds { get; set; }

        [NotMapped]
        public List<ReactSelectListItem> UsernamesWithIds { get; set; }

        [NotMapped]
        public List<ProjectMember> ProjectMembers { get; set; }
        
        [NotMapped]
        public List<ProjectAction> ProjectActions { get; set; }

    }
}
