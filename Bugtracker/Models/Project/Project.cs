using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Audit.EntityFramework;

namespace Bugtracker.Models
{
    [AuditIgnore]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }


        public DateTime ProjectCreated { get; set; }

        public IList<Tickets> ProjectTickets { get; set; }

        public IList<UserProject> UserProject { get; set; }


    }
}
