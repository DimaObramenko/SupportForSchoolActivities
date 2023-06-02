using Microsoft.AspNetCore.Mvc;
using SupportForSchoolActivities.Models.LoginModels;

namespace SupportForSchoolActivities.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel loginModel)
        {
            return View();
        }
    }
}
