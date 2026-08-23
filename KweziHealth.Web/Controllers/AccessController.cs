using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KweziHealth.Web.Models;
using KweziHealth.Web.ViewModels; 

namespace KweziHealth.Web.Controllers
{
    // Deliverable 3
    /// <summary>
    /// Manages the authentication flow for enterprise administrators.
    /// </summary>
    public class AccessController : Controller
    {
        private readonly SignInManager<SystemAdmin> _signInManager;

        public AccessController(SignInManager<SystemAdmin> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Displays the login page. Redirects to the dashboard if already authenticated.
        /// </summary>
        [HttpGet]
        [AllowAnonymous] // Explicitly allow unauthenticated users
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Staff");
            }

            return View();
        }

        /// <summary>
        /// Processes the login credentials.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user using Identity
                var result = await _signInManager.PasswordSignInAsync(
                    model.Username, 
                    model.Password, 
                    isPersistent: false, 
                    lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Staff");
                }
                
                // If login fails, return a generic security message
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
            }

            // Return the view with the current model state 
            return View(model);
        }

        /// <summary>
        /// Securely signs the user out and destroys the authentication cookie.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Access");
        }
    }
}