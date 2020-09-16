using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Audit.Core;
using Audit.EntityFramework;
using Bugtracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Data
{
    [AuditDbContext(Mode = AuditOptionMode.OptIn, IncludeEntityObjects = false, AuditEventType = "{database}_{context}")]
    public class ApplicationDbContext : AuditIdentityDbContext<ApplicationUser>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
             Audit.Core.Configuration.Setup()
            .UseEntityFramework(x => x
            .AuditTypeExplicitMapper(m => m
             .Map<Tickets, AuditTickets>((ticket, auditTicket) =>
             {
                 auditTicket.TicketUpdated = ticket.TicketUpdated;
                 auditTicket.TicketId = ticket.TicketId;
             })
             )
            
            ); 

        }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Attachments> Attachments { get; set; }

        public DbSet<Tickets> Tickets { get; set; }

        public DbSet<AuditTickets> AuditTickets { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }

        public DbSet<UserTicket> UserTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Many to many with users and projects
            builder.Entity<UserProject>().HasKey(u => new { u.UserID, u.ProjectId });

            builder.Entity<UserProject>()
                .HasOne(u => u.ApplicationUser)
                .WithMany(u => u.UserProject)
                .HasForeignKey(u => u.UserID);

            builder.Entity<UserProject>()
                .HasOne(u => u.Project)
                .WithMany(u => u.UserProject)
                .HasForeignKey(u => u.ProjectId);

            //Many tickets to one project
            builder.Entity<Tickets>()
                .HasOne(p => p.Project)
                .WithMany(p => p.ProjectTickets)
                .IsRequired();

            //Many comments to one ticket
            builder.Entity<Comments>()
                .HasOne(t => t.Ticket)
                .WithMany(t => t.Comments)
                .IsRequired();

            //Many to many with users and tickets
            builder.Entity<UserTicket>().HasKey(u => new { u.UserID, u.TicketID});

            builder.Entity<UserTicket>()
                .HasOne(u => u.ApplicationUser)
                .WithMany(u => u.TicketDevelopers)
                .HasForeignKey(u => u.UserID);

            builder.Entity<UserTicket>()
                .HasOne(u => u.Ticket)
                .WithMany(u => u.TicketDevelopers)
                .HasForeignKey(u => u.TicketID);


        }
    }
}
