using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models.ProjectModels
{
    public class ProjectCreateModel
    {
        public List<ReactSelectListItem> UsernamesWithIds { get; set; }
    }
}
