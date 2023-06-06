using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.RegisterModels;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ISchoolClassService _schoolClassService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StudentController(IStudentService studentService, ISchoolClassService schoolClassService, 
            IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _studentService = studentService;
            _schoolClassService= schoolClassService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Student> students = await _studentService.GetAllStudents();
            students = students.OrderBy(s => s.SchoolClass.Name)
                .ThenBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert()
        {
            var schoolClasses = await _schoolClassService.GetAllClasses();
            StudentVM studentVM = new StudentVM()
            {
                Student = new UserRegister(),
                Parent = new UserRegister(),
                SchoolClassSelectList = schoolClasses.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(StudentVM studentVM)
        {
            var parent = _mapper.Map<Parent>(studentVM.Parent);
            var student = _mapper.Map<Student>(studentVM.Student);
            student.Parent = parent;
            student.SchoolClass = await _schoolClassService.GetClass(studentVM.SchoolClass);
            var result = await _userManager.CreateAsync(student, studentVM.Student.Password);
            if (result.Succeeded)
            {
                var role1 = await _roleManager.FindByNameAsync(WC.ParentRole);
                var role = await _roleManager.FindByNameAsync(WC.StudentRole);

                if (role != null && role1 != null)
                {
                    await _userManager.AddToRoleAsync(parent, role1.Name);
                    await _userManager.AddToRoleAsync(student, role.Name);
                    return RedirectToAction("Index");
                }
            }
            return View(studentVM);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(await _studentService.DeleteStudent(id))
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
