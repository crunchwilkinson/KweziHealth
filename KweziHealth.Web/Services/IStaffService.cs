using KweziHealth.Web.Models;

namespace KweziHealth.Web.Services
{
    // Deliverable 2

    /// <summary>
    /// Handles all business logic and database operations for staff records.
    /// </summary>
    public interface IStaffService
    {
        Task<IEnumerable<StaffMember>> GetAllStaffAsync();
        Task<StaffMember?> GetStaffByIdAsync(int id);
        Task AddStaffAsync(StaffMember staff);
        Task UpdateStaffAsync(StaffMember staff);
        Task DeleteStaffAsync(int id);
    }
}