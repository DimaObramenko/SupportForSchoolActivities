using Microsoft.AspNetCore.Mvc;

namespace SupportForSchoolActivities.Controllers.EntitiesControllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
