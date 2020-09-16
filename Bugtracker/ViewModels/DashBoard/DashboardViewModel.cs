using System;
using System.Collections.Generic;
using Bugtracker.Models;

namespace Bugtracker.ViewModels
{
    public class DashboardViewModel
    {

        public Project Project { get; set; }

        public int BugsCount { get; set; }

        public int FeatureCount { get; set; }

        public int OpenCount { get; set; }

        public int ClosedCount { get; set; }

        public int InprogressCount { get; set; }

        public int HighCount { get; set; }

        public int MediumCount { get; set; }

        public int LowCount { get; set; }

        public int TotalTickets { get; set; }

    }
}
