using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _teacherService = teacherService;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = await _teacherService.GetAllTeachers();         
            return View(teachers);
        }
        public async Task<IActionResult> CreateTeacher()
        {
            Teacher teacher = new Teacher()
            {
                FirstName = "Tea",
                LastName = "Tea",
                Subjects = new List<Subject>()
                {
                    new Subject()
                    {
                        Name = "Математика"
                    },
                    new Subject()
                    {
                        Name = "Фізика"
                    }
                }
            };
            if(await _teacherService.CreateTeacher(teacher))
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
