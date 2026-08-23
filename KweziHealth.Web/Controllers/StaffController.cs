using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KweziHealth.Web.Models;
using KweziHealth.Web.Services;

namespace KweziHealth.Web.Controllers
{
    [Authorize] 
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        // --- INDEX ---
        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.GetAllStaffAsync();
            return View(staffList);
        }

        // --- CREATE ---
        public IActionResult Create()
        {
            return View();
        }

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

        // --- EDIT ---
        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StaffMember staff)
        {
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

        // --- DELETE ---
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _staffService.DeleteStaffAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        // --- ERROR HANDLER ---
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}