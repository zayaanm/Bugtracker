using System;
using System.Collections.Generic;
using Bugtracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugtracker.ViewModels
{
    public class UserManagementViewModel
    {
        public Project Project { get; set; }

      
        public IList<UserProject> UserProjects { get; set; }

        
    }
}
