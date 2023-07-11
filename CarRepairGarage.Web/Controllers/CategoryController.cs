using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
