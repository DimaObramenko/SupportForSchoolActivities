using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
namespace SupportForSchoolActivities.Models.LoginModels
{
    public class LoginModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Необхідно вказати ім'я")]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Необхідно вказати пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
