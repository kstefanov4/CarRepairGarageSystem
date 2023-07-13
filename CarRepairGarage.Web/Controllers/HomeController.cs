namespace CarRepairGarage.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using CarRepairGarage.Services.Contracts;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IGarageService garageService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGarageService service)
        {
            _logger = logger;
            garageService = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = await garageService.GetAllGaragesAsync(3);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }

            return View();
        }
    }
}