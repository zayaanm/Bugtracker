using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;

namespace Bugtracker.Models
{
    [AuditIgnore]
    public class Comments
    {

        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }


        public string UserId { get; set; }

        public string UserEmail { get; set; }

        

        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public virtual Tickets Ticket { get; set; }


    }
}
