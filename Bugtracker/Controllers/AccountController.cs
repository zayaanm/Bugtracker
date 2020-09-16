using System.Threading.Tasks;
using Bugtracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bugtracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Json(true);
            }

            else
            {
                return Json($"Email {email} is already in use ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

       
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {

            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = registerModel.Email, Email = registerModel.Email, FirstName = registerModel.FirstName, LastName = registerModel.LastName};
                var result = await userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Projects", "ProjectSelect");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }



            return View(registerModel);
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {

            if (ModelState.IsValid)
            {

                
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);

                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Projects", "ProjectSelect");
                }

                
                 ModelState.AddModelError(string.Empty, "Invalid Credentials");
                
            }



            return View(login);
        }


        
    }
}
