using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;

namespace SupportForSchoolActivities.Controllers.EntitiesControllers
{
    public class SchoolClassController : Controller
    {
        private readonly ISchoolClassService _classService;

        public SchoolClassController(ISchoolClassService classService)
        {
            _classService = classService;
        }

        public async Task<IActionResult> CreateClass()
        {
            SchoolClass schoolClass = new SchoolClass()
            {
                ClassNumber = 1,
                Name = "1-А",
                Students = new List<Student>()
                {
                    new Student()
                    {
                        LastName = "St1-А",
                        FirstName = "St1-А"
                    },
                    new Student()
                    {
                        LastName = "S1-А",
                        FirstName = "S1-А"
                    },
                    new Student()
                    {
                        LastName = "Stu1-А",
                        FirstName = "Stu1-А"
                    }
                }
            };
            if(await _classService.CreateClass(schoolClass))
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }            
        }

        public async Task<IActionResult> DeleteClass()
        {
            if (await _classService.DeleteClass(2))
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
