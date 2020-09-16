using System;
using System.Collections.Generic;
using Bugtracker.Models;

namespace Bugtracker.ViewModels
{
    public class ViewProjects
    {
        public IList<UserProject> UserProjects { get; set; }

        public ApplicationUser User { get; set; }
       
    }
}
