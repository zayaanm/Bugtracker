using System;
using System.ComponentModel.DataAnnotations;

namespace Bugtracker.ViewModels
{
    public class RemoveDeveloperViewModel
    {
        public int TicketId { get; set; }

        [Required]
        public string DeveloperEmail { get; set; }

        public int ProjectId { get; set; }
    }
}