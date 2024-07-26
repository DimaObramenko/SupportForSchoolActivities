using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.ViewModels;
using SupportForSchoolActivities.Service.Interfaces.EntityInterfaces;
using System.Data;

namespace SupportForSchoolActivities.Controllers.EntitiesControllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly ISchoolClassService _classService;
        private readonly ISubjectService _subjectService;

        public ScheduleController(IScheduleService scheduleService, ISchoolClassService schoolClassService, ISubjectService subjectService)
        {
            _scheduleService = scheduleService;
            _classService = schoolClassService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            int max = 0;
            var schoolClasses = await _classService.GetAllClasses();
            for (int i = 1; i <= 11; i++)
            {
                var count = schoolClasses.Where(c => c.ClassNumber == i).Count();
                if (count > max)
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

        public async Task<IActionResult> ClassSchedule(int classId)
        {
            var schoolClass = await _classService.GetClass(classId);
            var subjects = await _subjectService.GetAllSubjects();
            var schedules = (await _scheduleService.GetAllSchedules())
                .Where(s => s.SchoolClass.Id == classId)
                .ToList();
            ScheduleVM scheduleVM = new ScheduleVM()
            {
                ClassId = schoolClass.Id,
                ClassName = schoolClass.Name,
                SubjectsSelectList = subjects.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                Schedules = schedules.ToList()
            };
            
            TempData.Put("key", schedules);
            return View(scheduleVM);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int classId, DayOfWeek day)
        {
            var allSchedules = TempData.Get<List<Schedule>>("key");
            var schedules = allSchedules.Where(s=>s.DayOfWeek== day).ToList();
            var subjects = await _subjectService.GetAllSubjects();

            ScheduleVM scheduleVM = new ScheduleVM()
            {
                ClassId = classId,
                SubjectsSelectList = subjects.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                DayOfWeek = day,
                Schedules = schedules
            };

            return View(scheduleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ScheduleVM scheduleVM)
        {
            var schedulesBeforeEdit = (await _scheduleService.GetAllSchedules())
                .Where(s => s.SchoolClass.Id == scheduleVM.ClassId &&
                s.DayOfWeek == scheduleVM.DayOfWeek)
                .ToList();

            var schoolClass = await _classService.GetClass(scheduleVM.ClassId);

            var form = HttpContext.Request.Form;
            var scheduleSubjectList = form.ToList();
            Dictionary<int, int> editScheduleDic = new Dictionary<int, int>();
            foreach (var item in scheduleSubjectList)
            {
                if(item.Value != String.Empty)
                {
                    if(int.TryParse(item.Value.ToString(), out int value) && int.TryParse(item.Key.ToString(), out int key))
                    {
                       editScheduleDic.Add(key, value);
                    }                   
                }
            }
            
            var scheduleAfterEdit = new List<Schedule>();
            foreach(var schedule in editScheduleDic)
            {
                scheduleAfterEdit.Add(new Schedule()
                {
                    SchoolClass = schoolClass,
                    DayOfWeek = scheduleVM.DayOfWeek,
                    LessonNumber = schedule.Key,
                    Subject = await _subjectService.GetSubject(schedule.Value),
                });
            }
            int countWithoutChanges = 0;
            int countWithUpdates = 0;
            int countAdding = 0;
            int countDeleting = 0;

            foreach (var schedule in schedulesBeforeEdit)
            {
                if(scheduleAfterEdit.Any(s=>
                    s.SchoolClass.Id == schedule.SchoolClass.Id &&
                    s.DayOfWeek == schedule.DayOfWeek &&
                    s.LessonNumber == schedule.LessonNumber))
                {

                }
                else
                {
                    await _scheduleService.DeleteSchedule(schedule.Id);
                    countDeleting++;
                }
            }

            foreach(var schedule in scheduleAfterEdit)
            {
                if(schedulesBeforeEdit.Any(s =>
                    s.SchoolClass.Id == schedule.SchoolClass.Id &&
                    s.DayOfWeek == schedule.DayOfWeek &&
                    s.LessonNumber == schedule.LessonNumber))
                {
                    if (schedulesBeforeEdit.Any(s =>
                        s.SchoolClass.Id == schedule.SchoolClass.Id &&
                        s.DayOfWeek == schedule.DayOfWeek &&
                        s.Subject.Name == schedule.Subject.Name &&
                        s.LessonNumber == schedule.LessonNumber))
                    {
                        countWithoutChanges++;
                    }
                    else
                    {
                        var updateSchedule = schedulesBeforeEdit.FirstOrDefault(s =>
                            s.SchoolClass.Id == schedule.SchoolClass.Id &&
                            s.DayOfWeek == schedule.DayOfWeek &&
                            s.LessonNumber == schedule.LessonNumber);
                        await _scheduleService.UpdateSchedule(updateSchedule.Id, schedule);
                        countWithUpdates++;
                    }
                }
                else
                {
                    await _scheduleService.CreateSchedule(schedule);
                    countAdding++;
                }
            }




            return RedirectToAction("Index");
        }

        public async Task CreateSomeSchedulesAsync()
        {
            var schoolClass1A = await _classService.GetClass(3);
            var schoolClass1B = await _classService.GetClass(4);
            var math = await _subjectService.GetSubject(1);
            var history = await _subjectService.GetSubject(7);
            var ukrainian = await _subjectService.GetSubject(3);
            List<Schedule> schedules = new List<Schedule>()
            {
                new Schedule()
                {
                    DayOfWeek = DayOfWeek.Monday,
                    LessonNumber = 1,
                    SchoolClass = schoolClass1A,
                    Subject = math
                },
                new Schedule()
                {
                    DayOfWeek = DayOfWeek.Monday,
                    LessonNumber = 3,
                    SchoolClass = schoolClass1A,
                    Subject = ukrainian
                },
                new Schedule()
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    LessonNumber = 5,
                    SchoolClass = schoolClass1B,
                    Subject = math
                }
            };
            var schedule1 = new Schedule()
            {
                DayOfWeek = DayOfWeek.Monday,
                LessonNumber = 4,
                SchoolClass = schoolClass1A,
                Subject = history

            };
            try
            {
                await _scheduleService.CreateSchedule(schedule1);
            }
            catch
            {

            }
        }
    }
}
