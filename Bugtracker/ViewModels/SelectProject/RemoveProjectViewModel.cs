using System;
using System.ComponentModel.DataAnnotations;
using Bugtracker.Models;

namespace Bugtracker.ViewModels
{
    public class RemoveProjectViewModel
    {
        [Required]
        public int ProjectId { get; set; }
    }
}
