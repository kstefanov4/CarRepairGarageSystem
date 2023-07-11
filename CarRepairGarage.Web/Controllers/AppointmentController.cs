using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
