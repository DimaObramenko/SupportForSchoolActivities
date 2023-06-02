using System.ComponentModel.DataAnnotations;

namespace SupportForSchoolActivities.Models.RegisterModels
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Необхідно вказати ім'я")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Необхідно вказати прізвище")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Необхідно вказати адресу електронної пошти")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Необхідно вказати пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        public string ConfirmPassword { get; set; }
    }
}
