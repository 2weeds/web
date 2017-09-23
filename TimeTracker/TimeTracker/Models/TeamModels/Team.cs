using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.TeamModels
{
    public class Team
    {

        [Key]
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
