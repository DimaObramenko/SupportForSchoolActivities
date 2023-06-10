using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class JournalVM
    {
        public string SchoolClassName { get; set; }
        public string SubjectName { get; set; }
        public List<Student> Students { get; set; }
        public List<DateTime> DayOfLessons { get; set; }
    }
}
