using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.ProjectModels
{
    public class Project
    {

        [Key]
        public string Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [NotMapped]
        public List<ReactSelectListItem> ProjectMemberIds { get; set; }

        public virtual IEnumerable<ProjectMember> ProjectMembers { get; set; }

        [NotMapped]
        public List<ReactSelectListItem> UsernamesWithIds { get; set; }
    }
}
