using Microsoft.AspNetCore.Mvc;

namespace SupportForSchoolActivities.Controllers
{
    public class DiaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
