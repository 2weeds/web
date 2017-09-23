using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.ProjectModels
{
    public class Timer
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
