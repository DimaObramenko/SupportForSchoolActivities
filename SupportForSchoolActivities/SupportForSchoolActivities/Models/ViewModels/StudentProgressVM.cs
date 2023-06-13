using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class StudentProgressVM
    {
        public Student Student { get; set; }
        public List<DateTime> DateList { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Grade> Grades { get; set; }

    }
}
