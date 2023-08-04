using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _carService.GetAllCarsAsync();
            return View(model);
        }
    }
}
