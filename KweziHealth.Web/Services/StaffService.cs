using System.Collections.Generic;
using System.Linq;
using KweziHealth.Web.Models;
using KweziHealth.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace KweziHealth.Web.Services
{
   public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _context;

        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffMember>> GetAllStaffAsync()
        {
            return await _context.StaffMembers.ToListAsync();
        }

        public async Task<StaffMember> GetStaffByIdAsync(int id)
        {
            return await _context.StaffMembers.FindAsync(id) 
                ?? throw new KeyNotFoundException($"Staff member with ID {id} not found.");
        }

        public async Task AddStaffAsync(StaffMember staff)
        {
            _context.StaffMembers.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStaffAsync(StaffMember staff)
        {
            _context.StaffMembers.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(int id)
        {
            var staff = await _context.StaffMembers.FindAsync(id);
            if (staff != null)
            {
                _context.StaffMembers.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }
    }
}