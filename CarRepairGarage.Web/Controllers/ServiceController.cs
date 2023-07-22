using CarRepairGarage.Data.Models;
using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var services = await serviceService.GetAllServiceAsync();

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
