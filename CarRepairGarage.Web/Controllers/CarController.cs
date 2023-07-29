using CarRepairGarage.Data.Models;
using CarRepairGarage.Services;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static CarRepairGarage.Common.NotificationsMessagesConstants;

namespace CarRepairGarage.Web.Controllers
{
    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.UserRole)]
    public class CarController : BaseController
    {
        private readonly ICarService _carService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarController(
            ICarService carService, 
            UserManager<ApplicationUser> userManager)
        {
            _carService = carService; 
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var model = await _carService.GetAllCarsByUserIdAsync(user.Id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddCarViewModel model = new AddCarViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _carService.AddCarAsync(model, user);
                TempData[SuccessMessage] = $"Your car {model.Make} {model.CarModel} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await _carService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This car does not exist!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            var car = await _carService.GetCarByIdAsync(id);

            if (car.UserId != user.Id.ToString())
            {
                TempData[ErrorMessage] = "You are not the owner of this car. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            return View(car);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddCarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var car = await _carService.GetCarByIdAsync(id);

            if (car == null)
            {
                TempData[ErrorMessage] = "Car does not exist!";
                return View(model);
            }

            if (car.UserId != user.Id.ToString())
            {
                TempData[ErrorMessage] = "You are not the owner of this car. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await _carService.Edit(id, model);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Remove(int id)
        {
            if ((await _carService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This car does not exist!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            var car = await _carService.GetCarByIdAsync(id);
            

            if (car.UserId != user.Id.ToString())
            {
                TempData[ErrorMessage] = "You are not the owner of this car. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            try
            {
                await _carService.Delete(id);
                TempData[SuccessMessage] = $"Your Car was successfully deleted.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
