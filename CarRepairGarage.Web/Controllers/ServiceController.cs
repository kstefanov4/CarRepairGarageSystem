using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;

    /// <summary>
    /// Controller responsible for handling service-related actions.
    /// </summary>
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceController"/> class.
        /// </summary>
        /// <param name="serviceService">The service service.</param>
        public ServiceController(IServiceService serviceService)
        {
            this._serviceService = serviceService;
        }

        /// <summary>
        /// Displays the list of services with pagination support.
        /// </summary>
        /// <param name="pageIndex">The current page index (optional, defaults to 1).</param>
        /// <returns>The view containing the list of services.</returns>
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
