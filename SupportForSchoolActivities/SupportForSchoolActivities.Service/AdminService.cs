using SupportForSchoolActivities.DAL.Interfaces;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportForSchoolActivities.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> CreateAdmin(Admin admin)
        {
            try
            {
                await _adminRepository.CreateAsync(admin);
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public async Task<bool> DeleteAdmin(string id)
        {
            var admin = await _adminRepository.GetAsync(id);
            if(admin == null)
            {
                return false;
            }
            try
            {
                await _adminRepository.DeleteAsync(admin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Admin> GetAdmin(string id)
        {
            var admin = await _adminRepository.GetAsync(id);
            return admin;
        }

        public async Task<List<Admin>> GetListAdmins()
        {
            var admins = await _adminRepository.SelectAsync();
            return admins;
        }

        public async Task<bool> UpdateAdmin(string id, Admin admin)
        {
            if(admin == null || id == null)
            {
                return false;
            }
            try
            {
                var oldAdmin = await _adminRepository.GetAsync(id);
                oldAdmin.FirstName = admin.FirstName;
                oldAdmin.LastName = admin.LastName;
                oldAdmin.Email = admin.Email;
                oldAdmin.PhoneNumber= admin.PhoneNumber;
                oldAdmin.UserName = admin.UserName;
                await _adminRepository.UpdateAsync(oldAdmin);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
