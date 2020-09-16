using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bugtracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bugtracker.ViewModels
{
    public class TicketsViewModel
    {
        public IList<Tickets> ProjectTickets { get; set; }

        public Project Project { get; set; }

        
    }
}
