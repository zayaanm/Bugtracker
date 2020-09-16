using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Models;
using Bugtracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using static Bugtracker.Helpers.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bugtracker.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IProjectRepo _projectRepo;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        


        public TicketController(IProjectRepo projectRepo, IAuthorizationService authorizationService, UserManager<ApplicationUser> _userManager, ApplicationDbContext applicationDbContext)

        {
            _projectRepo = projectRepo;
            _authorizationService = authorizationService;
            _applicationDbContext = applicationDbContext;
            userManager = _userManager;
            
        }

        public async Task<IActionResult> Tickets(int id)
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

            List<Tickets> projectTickets = _applicationDbContext.Tickets
                .Where(x => x.ProjectId == current.ProjectId)
                .ToList();

            TicketsViewModel ticketsViewModel = new TicketsViewModel

            {
                Project = current,
                ProjectTickets = projectTickets,

                
            };

            return View(ticketsViewModel);


        }

        [NoDirectAccess]
        public async Task<IActionResult> AddTickets(int projectId)
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

            List<Tickets> projectTickets = _applicationDbContext.Tickets
                .Where(x => x.ProjectId == current.ProjectId)
                .ToList();

            AddTicketsViewModel ticketsViewModel = new AddTicketsViewModel
            {
                projectId = projectId,
                userEmail = User.Identity.Name
            };


            return View(ticketsViewModel);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddTickets(int projectId, AddTicketsViewModel addTicketsViewModel, string userEmail)
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

            

            if (ModelState.IsValid)
            {

                Tickets ticket = new Tickets
                {
                    SubmitterEmail = userEmail,
                    ProjectId = projectId,
                    TicketPriority = addTicketsViewModel.TicketPriority,
                    TicketDescription = addTicketsViewModel.TicketDescription,
                    TicketCreated = DateTime.Now,
                    TicketName = addTicketsViewModel.TicketName,
                    TicketStatus = addTicketsViewModel.TicketStatus,
                    TicketType = addTicketsViewModel.TicketType,
                    Project = current,
                    TicketUpdated = DateTime.Now,
                    UpdatedEmail = userEmail,
                    DescriptionUpdated = true
                    
                };


                _applicationDbContext.Tickets.Add(ticket);
                _applicationDbContext.SaveChanges();


                return Json(new { isValid = true });

            }


            ModelState.AddModelError(string.Empty, "An error occured");

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddTickets", addTicketsViewModel) });


        }


        [NoDirectAccess]
        public async Task<IActionResult> EditTickets(int projectId, int ticketId)
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

            Tickets ticket = _applicationDbContext.Tickets
                .FirstOrDefault(x => x.TicketId == ticketId);
                
            EditTicketsViewModel editTicketsViewModel = new EditTicketsViewModel
            {
                projectId = projectId,
                ticketId = ticketId,
                TicketPriority = ticket.TicketPriority,
                TicketDescription = ticket.TicketDescription,
                TicketName = ticket.TicketName,
                TicketStatus = ticket.TicketStatus,
                TicketType = ticket.TicketType,
                
            };

            return View(editTicketsViewModel);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditTickets(int projectId, int ticketId, EditTicketsViewModel editTicketsViewModel)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userProject = _applicationDbContext.UserProjects
                .Where(x => x.ProjectId == projectId && x.UserID == userId)
                .FirstOrDefault();

            if (userProject != null)
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

            editTicketsViewModel.projectId = projectId;
            editTicketsViewModel.ticketId = ticketId;

            if (ModelState.IsValid)
            {

                if (userProject.UserRole == "Submitter")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "EditTickets", editTicketsViewModel) });
                }

                Tickets ticket = _applicationDbContext.Tickets
                .FirstOrDefault(x => x.TicketId == ticketId);

                bool isDifferent = false;

                
                _applicationDbContext.Tickets.Attach(ticket);


                if(ticket.TicketType != editTicketsViewModel.TicketType || ticket.TicketStatus != editTicketsViewModel.TicketStatus || ticket.TicketPriority != editTicketsViewModel.TicketPriority || ticket.TicketDescription != editTicketsViewModel.TicketDescription)
                {
                    ticket.TicketType = editTicketsViewModel.TicketType;
                    ticket.TicketStatus = editTicketsViewModel.TicketStatus;
                    ticket.TicketPriority = editTicketsViewModel.TicketPriority;
                    isDifferent = true;
                    
                }


                if (ticket.TicketDescription != editTicketsViewModel.TicketDescription)
                {
                    ticket.TicketDescription = editTicketsViewModel.TicketDescription;
                    ticket.DescriptionUpdated = true;
                }
                else
                {
                    ticket.DescriptionUpdated = false;
                }

                if(isDifferent == false)
                {
                    ModelState.AddModelError(string.Empty, "No changes made");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "EditTickets", editTicketsViewModel) });
                }

                ticket.UpdatedEmail = User.Identity.Name;
                ticket.TicketUpdated = DateTime.Now;

                _applicationDbContext.SaveChanges();

                return Json(new { isValid = true });


            }


            ModelState.AddModelError(string.Empty, "An error occured");

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "EditUsers", editTicketsViewModel) });


        }

        [NoDirectAccess]
        public async Task<IActionResult> DeleteTickets(int projectId, int ticketId)
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

            DeleteTicketsViewModel deleteUsersViewModel = new DeleteTicketsViewModel
            {
                ProjectId = projectId,
                TicketId = ticketId
            };


            return View(deleteUsersViewModel);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteTickets(int projectId, int ticketId, DeleteTicketsViewModel deleteTicketsViewModel)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userProject = _applicationDbContext.UserProjects
                .Where(x => x.ProjectId == projectId && x.UserID == userId)
                .FirstOrDefault();

            if (userProject != null)
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

            deleteTicketsViewModel.ProjectId = projectId;
            deleteTicketsViewModel.TicketId = ticketId;

            if (ModelState.IsValid)
            {
                
                if(userProject.UserRole == "Submitter")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "DeleteTickets", deleteTicketsViewModel) });

                }


                Tickets ticket = _applicationDbContext.Tickets
                .FirstOrDefault(x => x.TicketId == ticketId);


                _applicationDbContext.Tickets.Remove(ticket);
                _applicationDbContext.UserTickets.RemoveRange(_applicationDbContext.UserTickets.Where(x => x.TicketID == ticketId));
                _applicationDbContext.Comments.RemoveRange(_applicationDbContext.Comments.Where(x => x.TicketId == ticketId));
                _applicationDbContext.Attachments.RemoveRange(_applicationDbContext.Attachments.Where(x => x.TicketId == ticketId));

                _applicationDbContext.SaveChanges();


                return Json(new { isValid = true });

            }


            ModelState.AddModelError(string.Empty, "An error occured");

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "DeleteTickets", deleteTicketsViewModel) });


        }

        
        public async Task<IActionResult> IndividualTicket(int projectId, int ticketId)
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

            Tickets ticket = _applicationDbContext.Tickets
                .FirstOrDefault(x => x.TicketId == ticketId);

            List<UserTicket> TicketDevelopers = _applicationDbContext.UserTickets
               .Include(item => item.ApplicationUser)
               .Where(tid => tid.TicketID == ticketId)
               .ToList();

            List<Comments> comments = _applicationDbContext.Comments
                .Where(x => x.TicketId == ticketId)
                .ToList();

            List<Attachments> attachments = _applicationDbContext.Attachments
                .Where(x => x.TicketId == ticketId)
                .Select(a => new Attachments
                {
                    UserEmail = a.UserEmail,
                    ContentName = a.ContentName,
                    CreatedOn = a.CreatedOn,
                    AttachmentId = a.AttachmentId
                })
                .ToList();

            List<AuditTickets> audits = _applicationDbContext.AuditTickets
                .Where(x => x.TicketId == ticketId)
                .ToList();

            IndividualTicketViewModel individualTicketViewModel = new IndividualTicketViewModel
            {
                Project = current,
                Ticket = ticket,
                TicketDevelopers = TicketDevelopers,
                Comments = comments,
                Attachments = attachments,
                AuditTickets = audits,
                AddAttachmentViewModel = new AddAttachmentViewModel() { TicketId = ticketId}
            };

            return View(individualTicketViewModel);


        }

        [NoDirectAccess]
        [HttpGet]
        public IActionResult AddDevelopers(int ticketId, int projectId)
        {
            AddDeveloperViewModel addDeveloperViewModel = new AddDeveloperViewModel
            {
                TicketId = ticketId,
                ProjectId = projectId
                
            };

            List<UserProject> userproject = _applicationDbContext.UserProjects
                .Include(user => user.ApplicationUser)
                .Where(x => x.ProjectId == projectId && x.UserRole != "Submitter")
                .ToList();

            List<string> users = new List<string>();

            foreach (var userprojects in userproject)
            {
                users.Add(userprojects.ApplicationUser.Email);
            }

            ViewBag.users = users;

            return View(addDeveloperViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddDevelopers(int projectId,int ticketId, AddDeveloperViewModel addDeveloperViewModel)
        {
            addDeveloperViewModel.TicketId = ticketId;

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userCredentials = _applicationDbContext.UserProjects
                .Where(x => x.ProjectId == projectId && x.UserID == userId)
                .FirstOrDefault();

            if (userCredentials != null)
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

            if (ModelState.IsValid)
            {
                if (userCredentials.UserRole == "Submitter" || userCredentials.UserRole == "Developer")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddDevelopers", addDeveloperViewModel) });

                }

                var userToAdd = await userManager.FindByNameAsync(addDeveloperViewModel.DeveloperEmail);


                UserProject userProject = _applicationDbContext.UserProjects
                    .FirstOrDefault(x => x.ProjectId == projectId && x.UserID == userToAdd.Id && x.UserRole != "Submitter");

                if(userProject == null)
                {
                    ModelState.AddModelError(string.Empty, "User to add has invalid permissions");
                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddDevelopers", addDeveloperViewModel) });
                }

                Tickets ticket = _applicationDbContext.Tickets
                    .FirstOrDefault(x => x.TicketId == ticketId);

                UserTicket checkifExists = _applicationDbContext.UserTickets
                    .Where(x => x.UserID == userToAdd.Id && x.TicketID == ticketId)
                    .FirstOrDefault();

                if (checkifExists != null)
                {
                    ModelState.AddModelError(string.Empty, "User already assigned");
                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddDevelopers", addDeveloperViewModel) });
                }

                UserTicket userTicket = new UserTicket
                {
                    ApplicationUser = userToAdd,
                    UserID = userToAdd.Id,
                    Ticket = ticket,
                    TicketID = ticketId,
                    ProjectId = projectId
                };

                    
                 _applicationDbContext.UserTickets.Add(userTicket);
                 _applicationDbContext.SaveChanges();

                 return Json(new { isValid = true });

            }

            ModelState.AddModelError(string.Empty, "An error occured");
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddDevelopers", addDeveloperViewModel) });
        }

        [NoDirectAccess]
        [HttpGet]
        public IActionResult RemoveDevelopers(int ticketId, int projectId)
        {

            RemoveDeveloperViewModel removeDeveloperViewModel = new RemoveDeveloperViewModel
            {
                ProjectId = projectId,
                TicketId = ticketId,
            };

            List<UserTicket>  userTickets = _applicationDbContext.UserTickets
                .Include(user => user.ApplicationUser)
                .Where(x => x.ProjectId == projectId && x.TicketID == ticketId)
                .ToList();

            List<string> users = new List<string>();

            foreach (var userticket in userTickets)
            {
                users.Add(userticket.ApplicationUser.Email);
            }

            ViewBag.users = users;

            return View(removeDeveloperViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveDevelopers(int projectId, int ticketId, RemoveDeveloperViewModel removeDeveloperViewModel)
        {
            removeDeveloperViewModel.TicketId = ticketId;

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userCredentials = _applicationDbContext.UserProjects
                .Where(x => x.ProjectId == projectId && x.UserID == userId)
                .FirstOrDefault();

            if (userCredentials != null)
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

            if (ModelState.IsValid)
            {
                if (userCredentials.UserRole == "Submitter" || userCredentials.UserRole == "Developer")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "RemoveDevelopers", removeDeveloperViewModel) });

                }

                var userToRemove = await userManager.FindByNameAsync(removeDeveloperViewModel.DeveloperEmail);


                UserTicket remove = _applicationDbContext.UserTickets
                    .Where(x => x.TicketID == ticketId && x.UserID == userToRemove.Id)
                    .FirstOrDefault();

                if (remove == null)
                {
                    ModelState.AddModelError(string.Empty, "An error occured");
                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "RemoveDevelopers", removeDeveloperViewModel) });
                }

                _applicationDbContext.UserTickets.Remove(remove);
                _applicationDbContext.SaveChanges();

                return Json(new { isValid = true });

            }

            ModelState.AddModelError(string.Empty, "An error occured");
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "RemoveDevelopers", removeDeveloperViewModel) });
        }


        [NoDirectAccess]
        [HttpGet]
        public IActionResult AddComment(int ticketId)
        {
            AddCommentViewModel addCommentViewModel = new AddCommentViewModel
            {
                TicketId = ticketId
            };

            
            return View(addCommentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int ticketId, AddCommentViewModel addCommentViewModel)
        {
            addCommentViewModel.TicketId = ticketId;

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                Tickets ticket = _applicationDbContext.Tickets
                .FirstOrDefault(x => x.TicketId == ticketId);

                Comments comment = new Comments
                {
                    TicketId = ticketId,
                    Text = addCommentViewModel.Comment,
                    Ticket = ticket,
                    CreatedOn = DateTime.Now,
                    UserEmail = user.Email,
                    UserId = user.Id

                };

                _applicationDbContext.Comments.Add(comment);
                _applicationDbContext.SaveChanges();

                return Json(new { isValid = true });
            };

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddComment", addCommentViewModel) });

        }


        [HttpGet]
        public IActionResult AddAttachment(int ticketId)
        {
            AddAttachmentViewModel addAttachmentViewModel = new AddAttachmentViewModel
            {
                TicketId = ticketId
            };


            return PartialView(addAttachmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttachment(int ticketId, AddAttachmentViewModel addAttachmentViewModel)
        {
            addAttachmentViewModel.TicketId = ticketId;

            if (ModelState.IsValid)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await addAttachmentViewModel.File.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var user = await userManager.GetUserAsync(HttpContext.User);

                        Tickets ticket = _applicationDbContext.Tickets
                        .FirstOrDefault(x => x.TicketId == ticketId);

                        Attachments attachment = new Attachments
                        {
                            Ticket = ticket,
                            TicketId = ticket.TicketId,
                            CreatedOn = DateTime.Now,
                            Content = memoryStream.ToArray(),
                            UserEmail = user.Email,
                            UserId = user.Id,
                            ContentName = addAttachmentViewModel.File.FileName
                        };

                        _applicationDbContext.Attachments.Add(attachment);

                        _applicationDbContext.SaveChanges();

                        return Json(new { isValid = true });
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                        return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddAttachment", addAttachmentViewModel) });
                    }
                }
                

            };

            ModelState.AddModelError(string.Empty, "Please select a valid file");
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddAttachment", addAttachmentViewModel) });

        }

        [HttpPost]
        public IActionResult DownloadFile(int attachmentId)
        {
            Attachments attachments = _applicationDbContext.Attachments.
                FirstOrDefault(x => x.AttachmentId == attachmentId);

            byte[] contents = attachments.Content;

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(attachments.ContentName, out contentType);

            return File(contents, contentType ?? "application/octet-stream", attachments.ContentName);

        }

    }
}
