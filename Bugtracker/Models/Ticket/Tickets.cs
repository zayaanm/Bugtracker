using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;

namespace Bugtracker.Models
{
    [AuditInclude]
    public class Tickets
    {
        [Key]
        public int TicketId { get; set; }

        [AuditIgnore]
        [Required]
        public string TicketName { get; set; }

        [AuditIgnore]
        public string TicketDescription { get; set; }

        [AuditIgnore]
        public DateTime TicketCreated { get; set; }

        
        public DateTime? TicketUpdated { get; set; }

        [Required]
        public string TicketPriority { get; set; }

        [Required]
        public string TicketType { get; set; }

        [Required]
        public string TicketStatus { get; set; }

        [AuditIgnore]
        public string SubmitterEmail { get; set; }

        [AuditIgnore]
        public IList<Comments> Comments { get; set; }

        [AuditIgnore]
        public int ProjectId { get; set; }

        [AuditIgnore]
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [AuditIgnore]
        public IList<UserTicket> TicketDevelopers { get; set; }

        public string UpdatedEmail { get; set; }

        public bool DescriptionUpdated { get; set; }
    }
}
