using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
