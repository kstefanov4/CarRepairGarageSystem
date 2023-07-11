using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    public class GarageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
