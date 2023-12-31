﻿namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Car;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;


    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.UserRole)]
    public class CarController : BaseController
    {
        private readonly ICarService _carService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Controller responsible for handling car-related actions, such as adding, editing, and removing cars.
        /// Users must be authenticated and have the "User" role to access these actions.
        /// </summary>
        public CarController(
            ICarService carService, 
            UserManager<ApplicationUser> userManager)
        {
            _carService = carService; 
            _userManager = userManager;
        }

        /// <summary>
        /// Displays the list of cars owned by the currently logged-in user.
        /// </summary>
        /// <returns>The view containing the list of cars.</returns>
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

        /// <summary>
        /// Displays the form to add a new car.
        /// </summary>
        /// <returns>The view containing the form to add a new car.</returns>
        [HttpGet]
        public IActionResult Add()
        {
            AddCarViewModel model = new AddCarViewModel();

            return View(model);
        }

        /// <summary>
        /// Handles the submission of the form to add a new car.
        /// </summary>
        /// <param name="model">The data submitted from the form.</param>
        /// <returns>Redirects to the car list view if successful, otherwise returns the form view with validation errors.</returns>
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

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the form to edit an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to edit.</param>
        /// <returns>The view containing the form to edit the car.</returns>
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

        /// <summary>
        /// Handles the submission of the form to edit an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to edit.</param>
        /// <param name="model">The data submitted from the form.</param>
        /// <returns>Redirects to the car list view if successful, otherwise returns the form view with validation errors.</returns>
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
            
            try
            {
                await _carService.Edit(id, model);
                TempData[SuccessMessage] = $"Your Car was successfully edited.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Removes an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to remove.</param>
        /// <returns>Redirects to the car list view if successful, otherwise returns the car list view with an error message.</returns>
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
                await _carService.DeleteAsync(id);
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
