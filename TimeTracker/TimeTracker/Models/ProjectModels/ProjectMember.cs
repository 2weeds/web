﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
