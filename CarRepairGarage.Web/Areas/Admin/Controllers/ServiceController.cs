using CarRepairGarage.Services;
using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _serviceService.GetAllServiceAsync();
            return View(model);
        }
    }
}
