using SupportForSchoolActivities.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service.Interfaces
{
    public interface IAdminService
    {
        Task<List<Admin>> GetListAdmins();
        Task<Admin> GetAdmin(string id);
        Task<bool> CreateAdmin(Admin admin);
        Task<bool> UpdateAdmin(string id, Admin admin);
        Task<bool> DeleteAdmin(string id);
    }
}
