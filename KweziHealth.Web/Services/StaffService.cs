using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KweziHealth.Web.Data;
using KweziHealth.Web.Models;

namespace KweziHealth.Web.Services
{
    // Deliverable 2
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _context;

        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all staff members. Uses AsNoTracking for read-only performance optimization.
        /// </summary>
        public async Task<IEnumerable<StaffMember>> GetAllStaffAsync()
        {
            return await _context.StaffMembers.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves a single staff member by their ID. Returns null if not found.
        /// </summary>
        public async Task<StaffMember?> GetStaffByIdAsync(int id)
        {
            return await _context.StaffMembers.FindAsync(id);
        }

        /// <summary>
        /// Adds a new staff member to the database.
        /// </summary>
        public async Task AddStaffAsync(StaffMember staff)
        {
            _context.StaffMembers.Add(staff);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing staff member's details.
        /// </summary>
        public async Task UpdateStaffAsync(StaffMember staff)
        {
            _context.StaffMembers.Update(staff);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a staff member by ID if they exist.
        /// </summary>
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