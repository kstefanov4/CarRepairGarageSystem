﻿namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;

    /// <summary>
    /// Controller responsible for handling garage-related actions.
    /// </summary>
    public class GarageController : Controller
    {
        private readonly IGarageService _garageService;
        private readonly ICategoryService _categoryService;
        private readonly IServiceService _serviceService;
        private readonly ICityService _cityService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GarageController"/> class.
        /// </summary>
        /// <param name="garageService">The garage service.</param>
        /// <param name="categoryService">The category service.</param>
        /// <param name="serviceService">The service service.</param>
        /// <param name="cityService">The city service.</param>
        /// <param name="userManager">The user manager.</param>
        public GarageController(
            IGarageService garageService,
            ICategoryService categoryService,
            IServiceService serviceService,
            ICityService cityService,
            UserManager<ApplicationUser> userManager)
        {
            _garageService = garageService;
            _categoryService = categoryService;
            _serviceService = serviceService;
            _cityService = cityService;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays the list of all garages with optional filtering and pagination.
        /// </summary>
        /// <param name="queryModel">The query model containing filter and pagination parameters.</param>
        /// <returns>The view containing the list of garages.</returns>
        public async Task<IActionResult> Index([FromQuery] AllGaragesQueryModel queryModel)
        {
            AllGaragesFilteredAndPagedServiceModel serviceModel = await _garageService.AllAsync(queryModel);

            queryModel.Garages = serviceModel.Garages;
            queryModel.TotalGarages = queryModel.TotalGarages;
            queryModel.Categories = await _categoryService.AllCategoriesNameAsync();
            queryModel.Services = await _serviceService.AllServicesNameAsync();
            queryModel.Cities = await _cityService.AllCitiesNameAsync();

            return View(queryModel);
        }

        /// <summary>
        /// Displays the list of garages owned by the currently logged-in user.
        /// </summary>
        /// <returns>The view containing the list of garages owned by the user.</returns>
        public async Task<IActionResult> AllByOwner()
        {
            string userId = _userManager.GetUserId(User);
            var model = await _garageService.GetAllGaragesByOwnerAsync(userId);
            return View(model);
        }

        /// <summary>
        /// Displays the list of garages filtered by the specified category.
        /// </summary>
        /// <param name="id">The ID of the category to filter by.</param>
        /// <returns>The view containing the filtered list of garages.</returns>
        public async Task<IActionResult> ByCategory(int id)
        {
            string category = await _categoryService.GetCategoryByIdAsync(id);

            var queryString = new Dictionary<string, string>
            {
                { "currentPage", "1" }, // Set the default value for currentPage
                { "category", category },
                { "sorting", "0" } // Set the default value for sorting
            };

            string url = Url.Action("Index", "Garage", queryString)!;
            return Redirect(url);

        }

        /// <summary>
        /// Displays the list of garages filtered by the specified service.
        /// </summary>
        /// <param name="id">The ID of the service to filter by.</param>
        /// <returns>The view containing the filtered list of garages.</returns>
        public async Task<IActionResult> ByService(int id)
        {
            string service = await _serviceService.GetServiceByIdAsync(id);

            var queryString = new Dictionary<string, string>
            {
                { "currentPage", "1" }, // Set the default value for currentPage
                { "service", service },
                { "sorting", "0" } // Set the default value for sorting
            };

            string url = Url.Action("Index", "Garage", queryString)!;
            return Redirect(url);

        }

        /// <summary>
        /// Displays detailed information about a specific garage.
        /// </summary>
        /// <param name="id">The ID of the garage to show details for.</param>
        /// <returns>The view containing detailed information about the garage.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            GarageViewModel model = await _garageService.GetGarageByIdAsync(id);
            return View(model);
        }
    }
}
