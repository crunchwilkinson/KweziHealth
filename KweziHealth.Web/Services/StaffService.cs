using System.Collections.Generic;
using System.Linq;
using KweziHealth.Web.Models;
using KweziHealth.Web.Data;

namespace KweziHealth.Web.Services
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _context;

        // Inject the ApplicationDbContext via the constructor
        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StaffMember> GetAllStaff()
        {
            return _context.StaffMembers.ToList();
        }

        public StaffMember GetStaffById(int id)
        {
            return _context.StaffMembers.Find(id); // .Find is highly optimized for primary keys
        }

        public void AddStaff(StaffMember staff)
        {
            _context.StaffMembers.Add(staff);
            _context.SaveChanges();
        }

        public void UpdateStaff(StaffMember staff)
        {
            _context.StaffMembers.Update(staff);
            _context.SaveChanges();
        }

        public void DeleteStaff(int id)
        {
            var staff = _context.StaffMembers.Find(id);
            if (staff != null)
            {
                _context.StaffMembers.Remove(staff);
                _context.SaveChanges();
            }
        }
    }
}