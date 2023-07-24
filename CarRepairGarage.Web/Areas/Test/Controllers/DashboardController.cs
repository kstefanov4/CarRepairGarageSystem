using CarRepairGarage.Data.Models;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Garage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Test.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IGarageService _garageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            IGarageService garageService,
            UserManager<ApplicationUser> userManager)
        {
            _garageService = garageService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            string userId = _userManager.GetUserId(User);
            IEnumerable<GarageViewModel> model = await _garageService.GetAllGaragesByOwnerAsync(userId);
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
