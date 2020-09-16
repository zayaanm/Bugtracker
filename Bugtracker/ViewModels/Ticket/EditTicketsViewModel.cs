using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugtracker.ViewModels
{
    public class EditTicketsViewModel
    {
        public int ticketId { get; set; }

        public int projectId { get; set; }

        public string userEmail { get; set; }

        [Required]
        public string TicketName { get; set; }

        [DataType(DataType.MultilineText)]
        public string TicketDescription { get; set; }

        [Required]
        public string TicketPriority { get; set; }

        [Required]
        public List<SelectListItem> Priority { get; } = new List<SelectListItem>

        {

        new SelectListItem { Value = "High", Text = "High" },
        new SelectListItem { Value = "Low", Text = "Low" },
        new SelectListItem { Value = "Medium", Text = "Medium"  },

        };

        [Required]
        public string TicketType { get; set; }

        public List<SelectListItem> Type { get; } = new List<SelectListItem>

        {

        new SelectListItem { Value = "Bug/Error", Text = "Bug/Error" },
        new SelectListItem { Value = "Requested Feature", Text = "Requested Feature" },

        };

        [Required]
        public string TicketStatus { get; set; }

        public List<SelectListItem> Status { get; } = new List<SelectListItem>

        {

        new SelectListItem { Value = "Open", Text = "Open" },
        new SelectListItem { Value = "Closed", Text = "Closed" },
        new SelectListItem { Value = "In Progress", Text = "In Progress" },

        };

    }
}

