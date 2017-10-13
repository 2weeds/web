using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.ProjectModels
{
    public class ProjectMember
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public int MemberRole { get; set; }

        [NotMapped]
        public virtual List<ProjectMemberAction> ProjectMemberActions { get; set; }

        [NotMapped]
        public bool IsCurrentUser { get; set; }

    }
}
