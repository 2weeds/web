using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.ProjectModels
{
    public class ProjectMemberAction
    {
        [Key]
        public string Id { get; set; }
        public string ProjectMemberId { get; set; }
        public string Description { get; set; }
    }
}
