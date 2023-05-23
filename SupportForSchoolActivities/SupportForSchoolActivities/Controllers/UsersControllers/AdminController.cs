using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> CreateAdmin()
        {
            var admins = await _adminService.GetListAdmins();
            var admin = admins.FirstOrDefault(a => a.FirstName == "TestFirst");

            if(admin == null)
            {
                return View();
            }
            else
            {
                if (await _adminService.DeleteAdmin(admin.Id) == true)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }                   
        }
    }
}
