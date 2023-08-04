using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    public class GarageController : BaseController
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _garageService.GetAllGaragesAsync(int.MaxValue);
            return View(model);
        }
    }
}
