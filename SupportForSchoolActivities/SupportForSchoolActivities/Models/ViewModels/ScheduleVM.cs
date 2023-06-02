using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class ScheduleVM
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public IEnumerable<SelectListItem> SubjectsSelectList { get; set; }
        public List<int> LessonNumbers { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
