using System.Collections.Generic;
using KweziHealth.Web.Models;

namespace KweziHealth.Web.Services
{
    public interface IStaffService
    {
        IEnumerable<StaffMember> GetAllStaff();
        StaffMember GetStaffById(int id);
        void AddStaff(StaffMember staff);
        void UpdateStaff(StaffMember staff);
        void DeleteStaff(int id);
    }
}