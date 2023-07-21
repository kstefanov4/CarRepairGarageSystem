namespace CarRepairGarage.Web.Controllers
{
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;
    using Microsoft.AspNetCore.Mvc;

    public class GarageController : Controller
    {
        private readonly IGarageService _garageService;
        private readonly ICategoryService _categoryService;
        private readonly IServiceService _serviceService;
        public GarageController(
            IGarageService garageService,
            ICategoryService categoryService,
            IServiceService serviceService)
        {
            _garageService = garageService;
            _categoryService = categoryService;
            _serviceService = serviceService;
        }
        public async Task<IActionResult> Index([FromQuery]AllGaragesQueryModel queryModel)
        {
            AllGaragesFilteredAndPagedServiceModel serviceModel = await _garageService.AllAsync(queryModel);

            queryModel.Garages = serviceModel.Garages;
            queryModel.TotalGarages = queryModel.TotalGarages;
            queryModel.Categories = await _categoryService.AllCategoriesNameAsync();
            queryModel.Services = await _serviceService.AllServicesNameAsync();

            /*var model = await _garageService.GetAllGaragesAsync(int.MaxValue);*/
            return View(queryModel);
        }
    }
}
