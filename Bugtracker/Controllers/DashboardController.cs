using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Models;
using Bugtracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bugtracker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IProjectRepo _projectRepo;
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _applicationDbContext;


        public DashboardController(IProjectRepo projectRepo, IAuthorizationService authorizationService, ApplicationDbContext applicationDbContext)

        {
            _projectRepo = projectRepo;
            _authorizationService = authorizationService;
            _applicationDbContext = applicationDbContext;
            
        }


        // GET: /<controller>/

        public async Task<IActionResult> Index(int id)
        {

            Project current = _projectRepo.getProject(id);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, current, "AccessProject");

            if (authorizationResult.Succeeded)
            {

            }

            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            List<Tickets> Tickets = _applicationDbContext.Tickets
               .Where(x => x.ProjectId == current.ProjectId)
               .ToList();

            //type data
            int bugsCount = Tickets.FindAll(ticket => ticket.TicketType == "Bug/Error").Count();
            int featureCount = Tickets.FindAll(ticket => ticket.TicketType == "Requested Feature").Count();

            //Status data
            int openCount = Tickets.FindAll(ticket => ticket.TicketStatus == "Open").Count();
            int closedCount = Tickets.FindAll(ticket => ticket.TicketStatus == "Closed").Count();
            int progressCount = Tickets.FindAll(ticket => ticket.TicketStatus == "In Progress").Count();

            //Priority data
            int highCount = Tickets.FindAll(ticket => ticket.TicketPriority == "High").Count();
            int lowCount = Tickets.FindAll(ticket => ticket.TicketPriority == "Low").Count();
            int mediumCount = Tickets.FindAll(ticket => ticket.TicketPriority == "Medium").Count();

            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                Project = current,
                FeatureCount = featureCount,
                BugsCount = bugsCount,
                OpenCount = openCount,
                ClosedCount = closedCount,
                InprogressCount = progressCount,
                HighCount = highCount,
                LowCount = lowCount,
                MediumCount = mediumCount,
                TotalTickets = Tickets.Count()
            };

            return View(dashboardViewModel);

        }

    }
}
