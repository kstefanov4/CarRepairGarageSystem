namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;

    public class GarageController : Controller
    {
        private readonly IGarageService _garageService;
        private readonly ICategoryService _categoryService;
        private readonly IServiceService _serviceService;
        private readonly ICityService _cityService;
        private readonly UserManager<ApplicationUser> _userManager;
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

        public async Task<IActionResult> AllByOwner()
        {
            string userId = _userManager.GetUserId(User);
            var model = await _garageService.GetAllGaragesByOwnerAsync(userId);
            return View(model);
        }
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            GarageViewModel model = await _garageService.GetGarageByIdAsync(id);
            return View(model);
        }
    }
}
