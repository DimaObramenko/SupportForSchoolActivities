using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class SubjectSchoolClassVM
    {
        public IEnumerable<SelectListItem> SchoolClassSelectList { get; set; }
        [Required]
        public int SchoolClassId { get; set; }
        public IEnumerable<SelectListItem> SubjectSelectList { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
