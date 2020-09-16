using System;
using System.Collections.Generic;
using Audit.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Bugtracker.Models
{
    [AuditIgnore]
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<UserProject> UserProject { get; set; }

        public IList<UserTicket> TicketDevelopers { get; set; }

    }
}
