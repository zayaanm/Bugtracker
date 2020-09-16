using System;
using System.ComponentModel.DataAnnotations;
using Bugtracker.Models;

namespace Bugtracker.ViewModels
{
    public class AddProjectViewModel
    {
        [Required]
        public string ProjectName { get; set; }

        [DataType(DataType.MultilineText)]
        public string ProjectDescription { get; set; }

    }
}
