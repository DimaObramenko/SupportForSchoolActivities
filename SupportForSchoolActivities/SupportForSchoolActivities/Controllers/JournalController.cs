using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.ViewModels;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;

namespace SupportForSchoolActivities.Controllers
{
    public class JournalController : Controller
    {
        private readonly ISchoolClassService _schoolClassService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;
        private readonly IScheduleService _scheduleService;

        public JournalController(ISubjectService subjectService, ISchoolClassService schoolClassService, IStudentService studentService, IScheduleService scheduleService)
        {
            _subjectService = subjectService;
            _schoolClassService = schoolClassService;
            _studentService = studentService;
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index(int schoolClassId)
        {
            var students = await _studentService.GetStudentsFromOneClass(schoolClassId);
            students = students.OrderBy(s => s.SchoolClass.Name)
                .ThenBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToList();
            
            var schedules = WC.Schedules;

            List<DayOfWeek> dayOfLessons = new List<DayOfWeek>();
            foreach (var schedule in schedules)
            {
                dayOfLessons.Add(schedule.DayOfWeek);
            }

            DateTime startDate = WC.BeginningOfSchoolYear; // Початкова дата
            DateTime endDate = WC.EndOfSchoolYear; // Кінцева дата

            List<DateTime> datesWithDayOfWeek = new List<DateTime>();

            foreach (DateTime date in EachDay(startDate, endDate))
            {
                if (dayOfLessons.Contains(date.DayOfWeek))
                {
                    int occurrences = dayOfLessons.Count(d => d == date.DayOfWeek);
                    for (int i = 0; i < occurrences; i++)
                    {
                        datesWithDayOfWeek.Add(date);
                    }
                }
            }
            string subjectName = schedules[0].Subject.Name;
            string schoolClassName = schedules[0].SchoolClass.Name;
            JournalVM journalVM = new JournalVM()
            {
                SchoolClassName= schoolClassName,
                SubjectName= subjectName,
                Students = students,
                DayOfLessons = datesWithDayOfWeek
            };
            WC.Students = students;
            return View(journalVM);
        }

        [HttpGet]
        public async Task<IActionResult> SelectJournal()
        {
            var schoolClasses = await _schoolClassService.GetAllClasses();
            schoolClasses = schoolClasses.OrderBy(s => s.ClassNumber)
                .ThenBy(s => s.Name)
                .ToList();
            var subjects = await _subjectService.GetAllSubjects();
            SubjectSchoolClassVM subjectClassVM = new SubjectSchoolClassVM()
            {
                SchoolClassSelectList = schoolClasses.Select(c=> new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                SubjectSelectList = subjects.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(subjectClassVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectJournal(SubjectSchoolClassVM subjectClassVM)
        {

            var allSchedules = await _scheduleService.GetAllSchedules();
            var schedSub = allSchedules.Where(s => s.Subject.Id == subjectClassVM.SubjectId);
            var schedClass = allSchedules.Select(s => s.SchoolClass.Id == subjectClassVM.SchoolClassId);

            var schedules = (await _scheduleService.GetAllSchedules())
                .Where(s => s.SchoolClass.Id == subjectClassVM.SchoolClassId &&
                s.Subject.Id == subjectClassVM.SubjectId)
                .ToList();

            WC.Schedules = schedules;
            return RedirectToAction("Index", new { schoolClassId = subjectClassVM.SchoolClassId });
        }

        [HttpGet]
        public async Task<IActionResult> EditGrades(DateTime date, string subjectName)
        {
            var subject = (await _subjectService.GetAllSubjects()).FirstOrDefault(s => s.Name == subjectName);
            List<Student> students = WC.Students;


            return RedirectToAction(nameof(SelectJournal));
        }


        public static IEnumerable<DateTime> EachDay(DateTime startDate, DateTime endDate)
        {
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                yield return date;
            }
        }
    }
}
