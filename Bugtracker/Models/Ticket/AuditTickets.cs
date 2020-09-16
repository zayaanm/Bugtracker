using System;
using System.ComponentModel.DataAnnotations;

namespace Bugtracker.Models
{
    public class AuditTickets
    {
        [Key]
        public int AuditId { get; set; }

        public int TicketId { get; set; }

   
        public DateTime? TicketUpdated { get; set; }

        
        public string TicketPriority { get; set; }

        
        public string TicketType { get; set; }

        
        public string TicketStatus { get; set; }

        public string UpdatedEmail { get; set; }

        public bool DescriptionUpdated { get; set; }
    }
}
