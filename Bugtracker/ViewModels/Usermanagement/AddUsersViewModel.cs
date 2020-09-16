using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugtracker.ViewModels
{
    public class AddUsersViewModel
    {
        public string UserEmail { get; set; }

        public string Role { get; set; }

        public int projectId { get; set; }

        public List<SelectListItem> RoleChoose { get; } = new List<SelectListItem>

        {

        new SelectListItem { Value = "Submitter", Text = "Submitter" },
        new SelectListItem { Value = "Developer", Text = "Developer" },
        new SelectListItem { Value = "Project Manager", Text = "Project Manager"  },
        new SelectListItem { Value = "Admin", Text = "Admin"  },

        };
    }
}
