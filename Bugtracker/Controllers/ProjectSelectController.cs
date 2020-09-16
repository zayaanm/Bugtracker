using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Models;
using Bugtracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Bugtracker.Helpers.Helper;

namespace Bugtracker.Controllers
{
    [Authorize]
    public class ProjectSelectController : Controller
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProjectRepo _projectRepo;

        public ProjectSelectController (ApplicationDbContext applicationDb, UserManager<ApplicationUser> userManager, IProjectRepo projectRepo)
        {
            _applicationDbContext = applicationDb;
            _userManager = userManager;
            _projectRepo = projectRepo;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Projects()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            List<UserProject> projects = _applicationDbContext.UserProjects
                .Include(item => item.Project)
                .Where(uid => uid.UserID == userId)
                .ToList();

            ViewProjects viewModel = new ViewProjects
            {
                UserProjects = projects,
                User = user
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            AddProjectViewModel addProjectViewModel = new AddProjectViewModel { };

            return PartialView(addProjectViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddProject(AddProjectViewModel addProjectViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                

                Project newProject = new Project
                {
                    ProjectName = addProjectViewModel.ProjectName,
                    ProjectDescription = addProjectViewModel.ProjectDescription,
                    ProjectCreated = DateTime.Now
                    
                };


                Project addedProject = _projectRepo.AddProject(newProject);

                UserProject userProject = new UserProject
                {
                    Project = addedProject,
                    ApplicationUser = user,
                    UserRole = "Project Creator",
                    DateAdded = DateTime.Now
                    
                };

                _applicationDbContext.UserProjects.Add(userProject);
                _applicationDbContext.SaveChanges();
                return Json(new { isValid = true });

            }

            ModelState.AddModelError(string.Empty, "An error occured");
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddAttachment", addProjectViewModel) });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProject()
        {
            RemoveProjectViewModel removeProjectViewModel = new RemoveProjectViewModel { };

            var user = await _userManager.GetUserAsync(HttpContext.User);

            List<Project> projects = _applicationDbContext.UserProjects
                .Include(item => item.Project)
                .Where(uid => uid.UserID == user.Id)
                .Select(x => x.Project)
                .ToList();


            ViewBag.projects = projects;

            return PartialView(removeProjectViewModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task <IActionResult> RemoveProject(RemoveProjectViewModel removeProjectViewModel)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(HttpContext.User);

                UserProject remove = _applicationDbContext.UserProjects
                    .Where(x => x.ProjectId == removeProjectViewModel.ProjectId && x.UserID == user.Id)
                    .FirstOrDefault();

                _applicationDbContext.UserProjects.Remove(remove);
                _applicationDbContext.SaveChanges();

                return Json(new { isValid = true });
            }

            ModelState.AddModelError(string.Empty, "An error occured");
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "RemoveProject", removeProjectViewModel) });

        }


    }
}
