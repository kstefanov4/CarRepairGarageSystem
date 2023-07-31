using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;

    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this._serviceService = serviceService;
        }
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var services = await _serviceService.GetAllServiceAsync();

            int pageSize = 6;
            int totalServices = services.Count;
            int totalPages = (int)Math.Ceiling(totalServices / (double)pageSize);

            pageIndex = Math.Max(1, pageIndex);
            pageIndex = Math.Min(pageIndex, totalPages);

            int startIndex = (pageIndex - 1) * pageSize;
            var displayedServices = services.Skip(startIndex).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageIndex;

            return View(displayedServices);
        }
    }
}
