using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using SupportForSchoolActivities.Service.Interfaces;
using SupportForSchoolActivities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace SupportForSchoolActivities.Controllers
{
    [Authorize(Roles = WC.StudentRole + "," + WC.ParentRole)]
    public class DiaryController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGradeService _gradeService;
        private readonly ISubjectService _subjectService;
        private readonly ISchoolClassService _schoolClassService;
        private readonly IScheduleService _scheduleService;
        private readonly IHomeworkService _homeworkService;

        public DiaryController(IStudentService studentService, IGradeService gradeService, ISubjectService subjectService, ISchoolClassService schoolClassService, IScheduleService scheduleService, IHomeworkService homeworkService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
            _subjectService = subjectService;
            _schoolClassService = schoolClassService;
            _scheduleService = scheduleService;
            _homeworkService = homeworkService;
        }
        public async Task<IActionResult> Index(string id, int weekOffset)
        {
            int offset = 0;
            if(weekOffset == 1)
            {
                offset = 1;
            }
            if(weekOffset == (-1) && WC.WeekStartDate != WC.BeginningOfSchoolYear)
            {
                offset = -1;
            }
            if(offset != 0)
            {
                DateTime currentWeekStartDate = WC.WeekStartDate;
                DateTime nextWeekStartDate = currentWeekStartDate.AddDays(offset * 7);
                WC.WeekStartDate = nextWeekStartDate;
                DateTime nextWeekEndDate = nextWeekStartDate.AddDays(6);
                WC.WeekEndDate = nextWeekEndDate;
            }

            var student = (await _studentService.GetAllStudents()).FirstOrDefault(s => s.Id == WC.StudentId);
            var schoolClass = (await _schoolClassService.GetClass(student.SchoolClass.Id));
            var schedules = (await _scheduleService.GetAllSchedules()).Where(s => s.SchoolClass.Id == schoolClass.Id).ToList();
            var grades = (await _gradeService.GetAllGrades())
                .Where(g => g.Student.Id == student.Id &&
                    g.Date >= WC.WeekStartDate &&
                    g.Date <= WC.WeekEndDate)
                .ToList();

            var homeworks = (await _homeworkService.GetAllHomeworks())
                .Where(h => h.SchoolClass.Id == schoolClass.Id &&
                    h.Deadline >= WC.WeekStartDate &&
                    h.Deadline <= WC.WeekEndDate)
                .ToList();

            List<DateTime> dates = new List<DateTime>();
            for (DateTime date = WC.WeekStartDate; date <= WC.WeekEndDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            DiaryVM diaryVM = new DiaryVM()
            {
                StudentId = student.Id,
                Schedules = schedules,
                Grades = grades,
                Homeworks = homeworks,
                Dates = dates
            };



            return View(diaryVM);
        }
    }
}
