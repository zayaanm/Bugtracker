using System;
using System.Collections.Generic;
using Bugtracker.Models;

namespace Bugtracker.ViewModels
{
    public class MyTicketsViewModel
    {
        public Project Project { get; set; }

        public IList<Tickets> AssignedTickets { get; set; }

        public IList<Tickets> SubmittedTickets { get; set; }

        public string UserRole { get; set; }
    }
}
