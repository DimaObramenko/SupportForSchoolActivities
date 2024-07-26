using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.LoginModels;
using SupportForSchoolActivities.Service.Interfaces;

namespace SupportForSchoolActivities.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IStudentService _studentService;
        private readonly IParentService _parentService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAdminService adminService, IStudentService studentService, IParentService parentService, ITeacherService teacherService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _adminService = adminService;
            _studentService = studentService;
            _parentService = parentService;
            _teacherService = teacherService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var student = (await _studentService.GetAllStudents()).FirstOrDefault(s => s.Email == loginModel.UserName);
                var admin = (await _adminService.GetListAdmins()).FirstOrDefault(s => s.Email == loginModel.UserName);
                var teacher = (await _teacherService.GetAllTeachers()).FirstOrDefault(s => s.Email == loginModel.UserName);
                var parent = (await _parentService.GetAllParents()).FirstOrDefault(s => s.Email == loginModel.UserName);
                var studentOfParent = (await _studentService.GetAllStudents()).FirstOrDefault(s => s.Parent.Email== loginModel.UserName);

                if (student != null && await _userManager.CheckPasswordAsync(student, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(student);
                    if (userRoles.Contains(WC.StudentRole))
                    {
                        await _signInManager.SignInAsync(student, isPersistent: false);
                        WC.StudentId = student.Id;
                        WC.TempRole = String.Empty;
                        return RedirectToAction("Index", "Diary", new { id = student.Id });
                    }
                }
                else if (admin != null && await _userManager.CheckPasswordAsync(admin, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(admin);
                    if (userRoles.Contains(WC.AdminRole))
                    {
                        WC.CurrentAdmin = admin;
                        await _signInManager.SignInAsync(admin, isPersistent: false);
                        return RedirectToAction("Index", "Student");
                    }
                }
                else if (teacher != null && await _userManager.CheckPasswordAsync(teacher, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(teacher);
                    if (userRoles.Contains(WC.TeacherRole))
                    {
                        WC.CurrentTeacher = teacher;
                        await _signInManager.SignInAsync(teacher, isPersistent: false);
                        return RedirectToAction("SelectJournal", "Journal");
                    }
                }
                else if (parent != null && await _userManager.CheckPasswordAsync(studentOfParent, loginModel.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(parent);
                    if (userRoles.Contains(WC.ParentRole))
                    {
                        WC.TempRole = WC.ParentRole;
                        WC.StudentId = studentOfParent.Id;
                        await _signInManager.SignInAsync(parent, isPersistent: false);
                        return RedirectToAction("Index", "Diary", new { id = parent.Students.FirstOrDefault() });
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Неправильні дані");

            return View(loginModel);
        }
    }
}
