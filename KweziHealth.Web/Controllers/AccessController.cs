using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KweziHealth.Web.Models;
using KweziHealth.Web.ViewModels;
using System.Threading.Tasks;

namespace KweziHealth.Web.Controllers
{
    public class AccessController : Controller
    {
        private readonly SignInManager<SystemAdmin> _signInManager;

        public AccessController(SignInManager<SystemAdmin> signInManager) 
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity == null)
            {
                return View();
            }

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Staff");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Staff");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Access");
        }
    }
}