using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class OneSchoolClassVM
    {
        public IEnumerable<Student> Students { get; set; }
        public int SchoolClassId { get; set; }
        public string SchoolClassName { get; set; }
    }
}
