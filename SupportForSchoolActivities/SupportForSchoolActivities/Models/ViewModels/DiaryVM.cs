using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class DiaryVM
    {
        public string StudentId { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Homework> Homeworks { get; set; }
        public List<DateTime> Dates { get; set; }
    }
}
