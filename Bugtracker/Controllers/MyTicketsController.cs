using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Models;
using Bugtracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bugtracker.Controllers
{
    public class MyTicketsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProjectRepo _projectRepo;
        private readonly IAuthorizationService _authorizationService;

        public MyTicketsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, IProjectRepo projectRepo, IAuthorizationService authorizationService)

        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _projectRepo = projectRepo;
            _authorizationService = authorizationService;

        }

        public async Task<IActionResult> Tickets(int projectId)
        {
            Project current = _projectRepo.getProject(projectId);

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

            var user = await _userManager.GetUserAsync(HttpContext.User);

            UserProject userProject = _applicationDbContext.UserProjects
                .FirstOrDefault(x => x.ProjectId == projectId && x.UserID == user.Id);

            string userRole = userProject.UserRole;

            List<Tickets> Submitted = _applicationDbContext.Tickets
               .Where(x => x.SubmitterEmail == user.Email && x.ProjectId == projectId)
               .ToList();

            MyTicketsViewModel myTicketsViewModel = new MyTicketsViewModel
            {
                UserRole = userRole,
                SubmittedTickets = Submitted,
                Project = current
            };

            if(userRole != "Submitter")
            {
                List<Tickets> Assigned = _applicationDbContext.UserTickets
               .Where(x => x.UserID == user.Id && x.ProjectId == projectId)
               .Select(x => x.Ticket)
               .ToList();

                myTicketsViewModel.AssignedTickets = Assigned;
            }


            return View(myTicketsViewModel);
        }

    }
}
