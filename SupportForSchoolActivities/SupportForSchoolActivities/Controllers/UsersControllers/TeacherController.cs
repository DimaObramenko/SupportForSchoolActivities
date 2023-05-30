using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.DAL;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.RegisterModels;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;
        private readonly ApplicationDbContext _db;
        public TeacherController(ITeacherService teacherService, UserManager<User> userManager,
            IMapper mapper, RoleManager<IdentityRole> roleManager, ISubjectService subjectService, ApplicationDbContext db)
        {
            _teacherService = teacherService;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _subjectService = subjectService;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teacher> teachers = await _teacherService.GetAllTeachers();         
            return View(teachers);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert()
        {
            var subjects = await _subjectService.GetAllSubjects();
            TeacherVM teacherVM = new TeacherVM()
            {
                Teacher = new UserRegister(),
                SubjectsSelectList = subjects.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
            };
            return View(teacherVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TeacherVM teacherVM)
        {
            /*teacher.Subjects = (await _subjectService.GetAllSubjects())
                .Where(s => teacherVM.SelectedSubjectsIds.Contains(s.Id))
                .ToList();
            */
            var teacher = _mapper.Map<Teacher>(teacherVM.Teacher);
            teacher.Subjects = _db.Subject.Where(s => teacherVM.SelectedSubjectsIds.Contains(s.Id)).ToList();

            /*var teacher = _mapper.Map<Teacher>(teacherVM.Teacher);
            var allSubjects = await _subjectService.GetAllSubjects();
            var subjects = allSubjects.Where(s => teacherVM.SelectedSubjectsIds.Contains(s.Id)).ToList();
            teacher.Subjects = subjects;*/
            
            var result = await _userManager.CreateAsync(teacher, teacherVM.Teacher.Password);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync(WC.TeacherRole);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(teacher, role.Name);
                    return RedirectToAction("Index");
                }
            }
            return View(teacherVM);
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
