using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Models.ViewModels;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System.Data;

namespace SupportForSchoolActivities.Controllers
{
    [Authorize(Roles = WC.StudentRole + "," + WC.ParentRole)]
    public class StudentProgressTableController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly ISchoolClassService _schoolClassService;
        private readonly IScheduleService _scheduleService;

        public StudentProgressTableController(IStudentService studentService, IGradeService gradeService, ISubjectService subjectService, ISchoolClassService schoolClassService, IScheduleService scheduleService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
            _subjectService = subjectService;
            _schoolClassService = schoolClassService;
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var student = (await _studentService.GetAllStudents()).FirstOrDefault(s => s.Id == WC.StudentId);
            var grades = (await _gradeService.GetAllGrades()).Where(g => g.Student.Id == student.Id).ToList();
            var schedules = (await _scheduleService.GetAllSchedules()).Where(s => s.SchoolClass.Id == student.SchoolClass.Id).ToList();

            var subjects = schedules.Select(s => s.Subject).Distinct().ToList();
            subjects = subjects.DistinctBy(s => s.Id).ToList();

            DateTime startDate = WC.BeginningOfSchoolYear;
            DateTime endDate = WC.EndOfSchoolYear;

            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    dates.Add(date);
                }
            }

            StudentProgressVM studentProgressVM = new StudentProgressVM()
            {
                Student = student,
                DateList = dates,
                Subjects = subjects,
                Grades = grades
            };


            return View(studentProgressVM);
        }
    }
}
