using System;
using Audit.EntityFramework;

namespace Bugtracker.Models
{
    [AuditIgnore]
    public class UserProject
    {
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string UserRole { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
