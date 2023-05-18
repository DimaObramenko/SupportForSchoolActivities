using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.DAL.Interfaces;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models;
using SupportForSchoolActivities.Service.Interfaces;
using System.Diagnostics;

namespace SupportForSchoolActivities.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IAdminService _adminService;

        public HomeController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /*public HomeController(IAdminRepository adminRepository, IStudentRepository studentRepository, IParentRepository parentRepository)
        {
            _adminRepository = adminRepository;
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
        }*/

        public async Task<IActionResult> Index()
        {
            Admin admin = new Admin()
            {
                FirstName = "Admin",
                LastName = "Adm",
            };
            if(await _adminService.CreateAdmin(admin) == true)
            {
                return RedirectToAction(nameof(Privacy));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
            //var students = await _studentRepository.SelectAsync();
            //var myS = students.Where(s => s.FirstName == "StudF");
            //var parent = new Parent()
            //{
            //    FirstName = "parent",
            //    LastName = "parenT",
            //    Students = new List<Student>()
            //    {
            //        new Student()
            //        {
            //            FirstName = "Petro",
            //            LastName = "Petrov"
            //        },
            //        myS.First()
            //    }
            //};
            //await _parentRepository.CreateAsync(parent);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}