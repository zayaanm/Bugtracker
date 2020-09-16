using System;
using System.ComponentModel.DataAnnotations;

namespace Bugtracker.Models
{
    public class Audit_Tickets
    {

        [Key]
        public int TicketId { get; set; }


        public string TicketName { get; set; }

        public string TicketDescription { get; set; }

        public DateTime? TicketUpdated { get; set; }


        public string TicketPriority { get; set; }


        public string TicketType { get; set; }

        [Required]
        public string TicketStatus { get; set; }

        public string SubmitterEmail { get; set; }



    }
}
