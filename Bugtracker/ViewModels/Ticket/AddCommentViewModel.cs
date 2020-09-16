using System;
using System.ComponentModel.DataAnnotations;

namespace Bugtracker.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public string Comment { get; set; }

        public int TicketId { get; set; }
    }
}
