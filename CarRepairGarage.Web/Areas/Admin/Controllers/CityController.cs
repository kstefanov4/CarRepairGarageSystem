using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    public class CityController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
