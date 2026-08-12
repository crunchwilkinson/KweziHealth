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

        public IActionResult Index()
        {
            var staffList = _staffService.GetAllStaff();
            return View(staffList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffMember staff)
        {
            if (ModelState.IsValid)
            {
                _staffService.AddStaff(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }
    }
}