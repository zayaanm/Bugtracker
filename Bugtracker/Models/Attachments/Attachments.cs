using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Bugtracker.Models
{
    [AuditIgnore]
    public class Attachments
    {
        [Key]
        public int AttachmentId { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ContentName { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }



        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public virtual Tickets Ticket { get; set; }
    }
}
