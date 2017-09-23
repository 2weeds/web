using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.ProjectModels
{
    public class Project
    {

        [Key]
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
