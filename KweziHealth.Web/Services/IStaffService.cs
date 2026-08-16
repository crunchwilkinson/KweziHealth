using System.Collections.Generic;
using KweziHealth.Web.Models;

namespace KweziHealth.Web.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffMember>> GetAllStaffAsync();
        Task<StaffMember> GetStaffByIdAsync(int id);
        Task AddStaffAsync(StaffMember staff);
        Task UpdateStaffAsync(StaffMember staff);
        Task DeleteStaffAsync(int id);
    }
}