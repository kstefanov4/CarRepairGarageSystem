using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
