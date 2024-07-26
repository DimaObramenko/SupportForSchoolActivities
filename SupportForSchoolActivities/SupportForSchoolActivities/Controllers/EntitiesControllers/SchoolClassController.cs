using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.ViewModels;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System.Data;
using System.Linq.Expressions;

namespace SupportForSchoolActivities.Controllers.EntitiesControllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class SchoolClassController : Controller
    {
        private readonly ISchoolClassService _classService;
        private readonly IStudentService _studentService;

        public SchoolClassController(ISchoolClassService classService, IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            int max = 0;
            var schoolClasses = await _classService.GetAllClasses();
            for (int i = 1; i <= 11; i++)
            {
                var count = schoolClasses.Where(c => c.ClassNumber == i).Count();
                if(count > max)
                {
                    max = count;
                }
            }
            SchoolClassVM schoolClassVM = new SchoolClassVM()
            {
                SchoolClasses = schoolClasses,
                MaxAmountOfClasses = max
            };

            return View(schoolClassVM);
        }

        public async Task<IActionResult> ShowClass(string name)
        {
            var students = await _studentService.GetAllStudents();
            var st = students.Where(s => s.SchoolClass.Name == name);
            var stu = students.Select(s => s.SchoolClass.Name == name);
            int p = 0;
            var studentList = new List<Student>();
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].SchoolClass.Name == name)
                {
                    studentList.Add(students[i]);
                    p++;
                }
            }
            studentList = studentList.OrderBy(s => s.SchoolClass.Name)
                .ThenBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();

            OneSchoolClassVM classVM = new OneSchoolClassVM()
            {
                Students = studentList,
                SchoolClassId = await _classService.GetIdClassByName(name),
                SchoolClassName = name
            };
            

            return View(classVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ukrLetter = new string[] { "А", "Б", "В", "Г", "Д" };
            List<SelectListItem> numbers = new List<SelectListItem>();
            for (int i = 1; i <= 11; i++)
            {
                numbers.Add(new SelectListItem() { 
                    Value = i.ToString(), 
                    Text = i.ToString() 
                });
            }

            List<SelectListItem> letters = new List<SelectListItem>();
            for (int i = 0; i < ukrLetter.Length; i++)
            {
                letters.Add(new SelectListItem()
                {
                    Value = ukrLetter[i],
                    Text= ukrLetter[i]
                });
            }

            CreateSchoolClassVM classVM = new CreateSchoolClassVM()
            {
                NumberClassSelectList = numbers,
                LettersClassSelectList= letters
                
            };

            return View(classVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSchoolClassVM classVM)
        {
            string className = $"{classVM.ClassNumber}-{classVM.ClassLetter}";
            var schoolClass = new SchoolClass()
            {
                Name = className,
                ClassNumber = classVM.ClassNumber
            };
            if(await _classService.CreateClass(schoolClass))
            {
                return RedirectToAction("Index");
            }           
            return View(classVM);
        }


        public async Task<IActionResult> Delete(int id)
        {
            if(await _classService.DeleteClass(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
