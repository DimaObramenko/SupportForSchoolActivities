using Microsoft.AspNetCore.Mvc.Rendering;

namespace SupportForSchoolActivities.Models.RegisterModels
{
    public class StudentVM
    {
        public UserRegister Student { get; set; }
        public UserRegister Parent { get; set; }
        public IEnumerable<SelectListItem> SchoolClassSelectList { get; set; }
        public int SchoolClass { get; set; }
    }
}
