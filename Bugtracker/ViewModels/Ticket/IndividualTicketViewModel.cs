using System;
using System.Collections.Generic;
using Bugtracker.Models;

namespace Bugtracker.ViewModels
{
    public class IndividualTicketViewModel
    {
        public Project Project { get; set; }

        public Tickets Ticket { get; set; }

        public IList<UserTicket> TicketDevelopers { get; set; }

        public IList<Comments> Comments { get; set; }

        public IList<Attachments> Attachments { get; set; }

        public IList<AuditTickets> AuditTickets { get; set; }

        public AddAttachmentViewModel AddAttachmentViewModel { get; set; }

    }
}
