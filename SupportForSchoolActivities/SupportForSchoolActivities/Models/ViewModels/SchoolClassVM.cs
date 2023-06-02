using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class SchoolClassVM
    {
        public IEnumerable<SchoolClass> SchoolClasses { get; set; }
        public int MaxAmountOfClasses { get; set; }
    }
}
