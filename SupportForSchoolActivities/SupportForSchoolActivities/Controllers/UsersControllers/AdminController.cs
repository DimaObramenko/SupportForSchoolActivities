using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.RegisterModels;
using SupportForSchoolActivities.Service.Interfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _adminService = adminService;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Admin> admins = await _adminService.GetListAdmins();
            return View(admins);
        }

        [HttpGet]
        public IActionResult Upsert()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UserRegister userRegister)
        {
            var admin = _mapper.Map<Admin>(userRegister);
            var result = await _userManager.CreateAsync(admin, userRegister.Password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync(WC.AdminRole);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(admin, role.Name);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Error", "Home");
        }

        public IActionResult Delete()
        {

        }
    }
}
