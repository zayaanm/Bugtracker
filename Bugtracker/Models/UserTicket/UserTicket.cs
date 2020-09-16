using System;
namespace Bugtracker.Models
{
    public class UserTicket
    {
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int TicketID { get; set; }
        public Tickets Ticket { get; set; }

        public int ProjectId { get; set; }
    }
}
