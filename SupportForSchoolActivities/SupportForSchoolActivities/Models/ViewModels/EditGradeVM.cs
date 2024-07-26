using Microsoft.AspNetCore.Mvc.Rendering;
using SupportForSchoolActivities.Domain.Entity;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class EditGradeVM
    {
        public List<Student> Students { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<SelectListItem> MarkSelectList { get; set; }
        public int SubjectId { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
