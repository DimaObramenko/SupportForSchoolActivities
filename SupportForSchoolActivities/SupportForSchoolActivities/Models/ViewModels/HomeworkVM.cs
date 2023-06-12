using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class HomeworkVM
    {
        public Homework Homework { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
