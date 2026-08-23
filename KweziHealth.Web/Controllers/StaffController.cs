using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KweziHealth.Web.Models;
using KweziHealth.Web.Services;

namespace KweziHealth.Web.Controllers
{
    // Deliverable 3
    /// <summary>
    /// Handles all Staff Operations. Locked down to authenticated users only.
    /// </summary>
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        /// <summary>
        /// Displays the dashboard directory of all staff members.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return View(staffList);
        }

        /// <summary>
        /// Displays the form to register a new staff member.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processes the registration of a new staff member.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffMember staff)
        {
            if (ModelState.IsValid)
            {
                await _staffService.AddStaffAsync(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        /// <summary>
        /// Displays the edit form for a specific staff member.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            
            // Returns a 404 error if the ID doesn't exist in the database
            if (staff == null)
            {
                return NotFound();
            }
            
            return View(staff);
        }

        /// <summary>
        /// Processes the updates to a staff member's record.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StaffMember staff)
        {
            // Security check to ensure the URL ID matches the submitted form ID
            if (id != staff.StaffId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _staffService.UpdateStaffAsync(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        /// <summary>
        /// Displays the confirmation page to delete a staff member.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            
            if (staff == null)
            {
                return NotFound();
            }
            
            return View(staff);
        }

        /// <summary>
        /// Executes the permanent deletion of a staff member.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _staffService.DeleteStaffAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the generic error page for the application.
        /// </summary>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}