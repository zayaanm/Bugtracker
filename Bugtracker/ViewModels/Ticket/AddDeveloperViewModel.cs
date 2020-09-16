using System;
namespace Bugtracker.ViewModels
{
    public class AddDeveloperViewModel
    {
        public int TicketId { get; set; }

        public string DeveloperEmail { get; set; }

        public int ProjectId { get; set; }
    }
}
