using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Bugtracker.ViewModels
{
    public class AddAttachmentViewModel
    {
        public int TicketId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
