using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Models;
using Bugtracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Bugtracker.Helpers.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bugtracker.Controllers
{
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly IProjectRepo _projectRepo;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _applicationDbContext;


        public ManageUsersController(IProjectRepo projectRepo, IAuthorizationService authorizationService, UserManager<ApplicationUser> _userManager, ApplicationDbContext applicationDbContext)

        {
            _projectRepo = projectRepo;
            _authorizationService = authorizationService;
            _applicationDbContext = applicationDbContext;
            userManager = _userManager;
        }


        [HttpGet]
        public async Task<IActionResult> UserManagement(int id)
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

            List<UserProject> userproject = _applicationDbContext.UserProjects
                .Include(user => user.ApplicationUser)
                .Where(x => x.ProjectId == current.ProjectId)
                .ToList();


            UserManagementViewModel userManagementViewModel = new UserManagementViewModel
            {
                Project = current,
                UserProjects = userproject,

            };

            return View(userManagementViewModel);
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddUsers(int projectId)
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

            AddUsersViewModel addUsersViewModel = new AddUsersViewModel { projectId = projectId };


            return View(addUsersViewModel);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddUsers(int projectId, [Bind("Role,UserEmail")] AddUsersViewModel addUsersViewModel)
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

            addUsersViewModel.projectId = projectId;

            if (ModelState.IsValid)
            {

                if(userProject.UserRole != "Project Creator" && userProject.UserRole != "Admin")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddUsers", addUsersViewModel) });
                }

                var userToAdd = await userManager.FindByNameAsync(addUsersViewModel.UserEmail);


                if (userToAdd != null)
                {
                    var checkifExists = _applicationDbContext.UserProjects
                    .Where(x => x.ProjectId == projectId && x.UserID == userToAdd.Id)
                    .FirstOrDefault();

                    if (checkifExists != null)
                    {

                        ModelState.AddModelError(string.Empty, "User Already in Project");

                        return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddUsers", addUsersViewModel) });
                    }

                    Project current = _projectRepo.getProject(projectId);

                    UserProject userproject = new UserProject
                    {
                        Project = current,
                        ApplicationUser = userToAdd,
                        UserRole = addUsersViewModel.Role,
                        DateAdded = DateTime.Now,

                    };


                    _applicationDbContext.UserProjects.Add(userproject);
                    _applicationDbContext.SaveChanges();



                    return Json(new { isValid = true, html = RenderRazorViewToString(this, "UserTablePartial") });
                }

            }


            ModelState.AddModelError(string.Empty, "User Not Found");

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddUsers", addUsersViewModel) });



        }

        [NoDirectAccess]
        public async Task<IActionResult> EditUsers(int projectId, string userId)
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

            EditUsersViewModel editUsersViewModel = new EditUsersViewModel { projectId = projectId, UserId = userId };


            return View(editUsersViewModel);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditUsers(int projectId, [Bind("Role,UserId")] EditUsersViewModel editUsersViewModel)
        {

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

            editUsersViewModel.projectId = projectId;

            if (ModelState.IsValid)
            {
                if (userCredentials.UserRole != "Project Creator" && userCredentials.UserRole != "Admin")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "EditUsers", editUsersViewModel) });
                }

                var userProject = _applicationDbContext.UserProjects
                 .Where(x => x.ProjectId == projectId && x.UserID == editUsersViewModel.UserId)
                  .FirstOrDefault();

                if (userProject.UserRole == "Project Creator")
                {
                    ModelState.AddModelError(string.Empty, "Can not change project creator's role");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "EditUsers", editUsersViewModel) });
                }

                userProject.UserRole = editUsersViewModel.Role;

                _applicationDbContext.UserProjects.Update(userProject);
                _applicationDbContext.SaveChanges();



                return Json(new { isValid = true, html = RenderRazorViewToString(this, "UserTablePartial") });


            }


            ModelState.AddModelError(string.Empty, "An error occured");

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "EditUsers", editUsersViewModel) });


        }

        public async Task<IActionResult> DeleteUsers(int projectId, string userId)
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

            DeleteUsersViewModel deleteUsersViewModel = new DeleteUsersViewModel { projectId = projectId, UserId = userId };


            return View(deleteUsersViewModel);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteUsers(int projectId, EditUsersViewModel deleteUsersViewModel)
        {

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

            deleteUsersViewModel.projectId = projectId;

            if (ModelState.IsValid)
            {
                if (userCredentials.UserRole != "Project Creator" && userCredentials.UserRole != "Admin")
                {
                    ModelState.AddModelError(string.Empty, "Invalid Permissions");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "DeleteUsers", deleteUsersViewModel) });
                }

                var userProject = _applicationDbContext.UserProjects
                 .Where(x => x.ProjectId == projectId && x.UserID == deleteUsersViewModel.UserId)
                  .FirstOrDefault();

                if (userProject.UserRole == "Project Creator")
                {
                    ModelState.AddModelError(string.Empty, "Can not delete project creator");

                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "DeleteUsers", deleteUsersViewModel) });
                }

                _applicationDbContext.UserProjects.Remove(userProject);
                _applicationDbContext.UserTickets.RemoveRange(_applicationDbContext.UserTickets.Where(x => x.ProjectId == projectId && x.UserID == userProject.UserID));
                _applicationDbContext.SaveChanges();


                return Json(new { isValid = true });

            }


            ModelState.AddModelError(string.Empty, "An error occured");

            return Json(new { isValid = false, html = RenderRazorViewToString(this, "DeleteUsers", deleteUsersViewModel) });


        }

       
    }
}
