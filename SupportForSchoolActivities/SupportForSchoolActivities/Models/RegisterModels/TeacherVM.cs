using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace SupportForSchoolActivities.Models.RegisterModels
{
    public class TeacherVM
    {
        public UserRegister Teacher { get; set; }
        public IEnumerable<SelectListItem> SubjectsSelectList { get; set; }
        public List<int> SelectedSubjectsIds { get; set; }
    }
}
