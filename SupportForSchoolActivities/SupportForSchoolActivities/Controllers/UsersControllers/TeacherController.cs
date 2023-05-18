using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Service.Interfaces;

namespace SupportForSchoolActivities.Controllers.UsersControllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
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
