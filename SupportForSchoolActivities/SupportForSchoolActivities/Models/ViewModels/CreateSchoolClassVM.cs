using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SupportForSchoolActivities.Models.ViewModels
{
    public class CreateSchoolClassVM
    {
        [Required(ErrorMessage = "Введіть назву класу")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Оберіть номер класу")]
        [Range(1, 11)]
        public int ClassNumber { get; set; }
        [Required(ErrorMessage = "Оберіть літеру класу")]
        public string ClassLetter { get; set; }
        public IEnumerable<SelectListItem> NumberClassSelectList { get; set; }
        public IEnumerable<SelectListItem> LettersClassSelectList { get; set; }
    }
}
