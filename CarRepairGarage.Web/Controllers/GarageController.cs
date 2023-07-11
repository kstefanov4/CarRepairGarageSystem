namespace CarRepairGarage.Web.Controllers
{
    using CarRepairGarage.Services.Contracts;

    using Microsoft.AspNetCore.Mvc;

    public class GarageController : Controller
    {
        private readonly IGarageService garageService;
        public GarageController(IGarageService garageService)
        {
            this.garageService = garageService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await garageService.GetAllGaragesAsync(int.MaxValue);
            return View(model);
        }
    }
}
