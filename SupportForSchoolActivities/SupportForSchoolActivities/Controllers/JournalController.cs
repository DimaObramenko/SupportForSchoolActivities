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
        private readonly IGradeService _gradeService;

        public JournalController(ISubjectService subjectService, ISchoolClassService schoolClassService, IStudentService studentService, IScheduleService scheduleService, IGradeService gradeService)
        {
            _subjectService = subjectService;
            _schoolClassService = schoolClassService;
            _studentService = studentService;
            _scheduleService = scheduleService;
            _gradeService = gradeService;
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
            var grades = (await _gradeService.GetAllGrades())
                .Where(g => g.Subject.Name == subjectName)
                .ToList();
            List<Grade> gradeList = new List<Grade>();
            foreach (var item in grades)
            {
                if(item.Student.SchoolClass.Id == schoolClassId)
                {
                    gradeList.Add(item);
                }
            }
            grades = grades.Where(g => g.Student.SchoolClass.Id == schoolClassId && g.Subject.Name == subjectName).ToList();
            JournalVM journalVM = new JournalVM()
            {
                SchoolClassName= schoolClassName,
                SubjectName= subjectName,
                Students = students,
                DayOfLessons = datesWithDayOfWeek,
                Grades= grades
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
            var allGrades = await _gradeService.GetAllGrades();
            var grades = allGrades.Where(g => g.Subject.Name == subjectName && g.Date == date).ToList();
            List<SelectListItem> marks = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                marks.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }

            EditGradeVM gradeVM = new EditGradeVM()
            {
                SubjectId = subject.Id,
                Students = students,
                Date = date,
                MarkSelectList = marks,
                Grades = grades
            };
            WC.DateForEditMark= date;

            return View(gradeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGrades(EditGradeVM gradeVM)
        {
            var date = WC.DateForEditMark;
            var gradesBeforeEditing = (await _gradeService.GetAllGrades())
                .Where(g => g.Date == date &&
                g.Subject.Id == gradeVM.SubjectId)
                .ToList();

            var form = HttpContext.Request.Form;
            var formList = form.ToList();
            Dictionary<string, int> editGradeDic = new Dictionary<string, int>();
            foreach (var item in formList)
            {
                if(item.Value != String.Empty)
                {
                    if (int.TryParse(item.Value.ToString(), out int value) && Guid.TryParse(item.Key.ToString(), out Guid key))
                    {
                        editGradeDic.Add(item.Key, value);
                    }
                }
            }
            var gradesAfterEditing = new List<Grade>();
            
            foreach (var grade in editGradeDic)
            {
                gradesAfterEditing.Add(new Grade()
                {
                    Student = await _studentService.GetStudent(grade.Key),
                    Subject = await _subjectService.GetSubject(gradeVM.SubjectId),
                    Date = date,
                    Mark = grade.Value
                });
            }
            int countWithoutChanges = 0;
            int countWithUpdates = 0;
            int countAdding = 0;
            int countDeleting = 0;

            foreach (var grade in gradesBeforeEditing)
            {
                if(gradesAfterEditing.Any(g=>
                    g.Date == grade.Date &&
                    g.Subject.Id == grade.Subject.Id &&
                    g.Student.Id == grade.Student.Id))
                {

                }
                else
                {
                    await _gradeService.DeleteGrade(grade.Id);
                    countDeleting++;
                }
            }

            foreach (var grade in gradesAfterEditing)
            {
                if (gradesBeforeEditing.Any(g =>
                    g.Date == grade.Date &&
                    g.Subject.Id == grade.Subject.Id &&
                    g.Student.Id == grade.Student.Id))
                {
                    if (gradesBeforeEditing.Any(g =>
                        g.Date == grade.Date &&
                        g.Subject.Id == grade.Subject.Id &&
                        g.Student.Id == grade.Student.Id &&
                        g.Mark == grade.Mark))
                    {
                        countWithoutChanges++;
                    }
                    else
                    {
                        var editGrade = gradesBeforeEditing.FirstOrDefault(g =>
                            g.Date == grade.Date &&
                            g.Subject.Id == grade.Subject.Id &&
                            g.Student.Id == grade.Student.Id);
                        await _gradeService.UpdateGrade(editGrade.Id, grade);
                        countWithUpdates++;
                    }
                }
                else
                {
                    await _gradeService.CreateGrade(grade);
                    countAdding++;
                }
            }


            return RedirectToAction("Index", new { schoolClassId = 3 });
            //return View(gradeVM);
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
