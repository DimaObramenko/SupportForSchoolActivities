using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.DAL;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.RegisterModels;
using SupportForSchoolActivities.Service;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System.Data;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    [Authorize(Roles = WC.AdminRole)]
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
            var teacher = _mapper.Map<Teacher>(teacherVM.Teacher);
            teacher.Subjects = _db.Subject.Where(s => teacherVM.SelectedSubjectsIds.Contains(s.Id)).ToList();
            
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

        public async Task<IActionResult> Delete(string id)
        {
            if (await _teacherService.DeleteTeacher(id))
            {
                return RedirectToAction("Index", "Teacher");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
