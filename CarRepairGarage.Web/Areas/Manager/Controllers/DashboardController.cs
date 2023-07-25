using CarRepairGarage.Data.Models;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Garage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IGarageService _garageService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceService _serviceService;
        public DashboardController(
            IGarageService garageService,
            UserManager<ApplicationUser> userManager,
            IServiceService serviceService)
        {
            _garageService = garageService;
            _userManager = userManager;
            _serviceService = serviceService;

        }
        [HttpGet]
        public async Task<IActionResult> All(int pageIndex = 1)
        {
            string userId = _userManager.GetUserId(User);
            IEnumerable<GarageViewModel> model = await _garageService.GetAllGaragesByOwnerAsync(userId);

            int pageSize = 1;
            int totalServices = model.Count();
            int totalPages = (int)Math.Ceiling(totalServices / (double)pageSize);

            pageIndex = Math.Max(1, pageIndex);
            pageIndex = Math.Min(pageIndex, totalPages);

            int startIndex = (pageIndex - 1) * pageSize;
            var displayedServices = model.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageIndex;

            return View(displayedServices);

        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddGarageViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddGarageViewModel model)
        {
            return View(model);
        }
    }
}
