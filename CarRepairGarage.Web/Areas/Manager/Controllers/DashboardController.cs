using CarRepairGarage.Data.Models;
using CarRepairGarage.Services;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Garage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static CarRepairGarage.Common.NotificationsMessagesConstants;

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
        public async Task<IActionResult> Modify(int id)
        {
            if ((await _garageService.Exists(id)) == false)
            {
                TempData[ErrorMessage] = $"You aretrying to modify non existing garage!";
                return RedirectToAction(nameof(All));
            }

            var user = await _userManager.GetUserAsync(User);
            var garage = await _garageService.GetGarageByIdAsync(id);

            if (garage.OwnerId != user.Id)
            {
                TempData[ErrorMessage] = "You are not the owner of this garage. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var model = await _garageService.ModifyGarageByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyGarageViewModel model)
        {

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddGarageViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGarageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _garageService.AddGarageAsync(model, user);
                TempData[SuccessMessage] = $"Your Garage {model.Name} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }
    }
}
