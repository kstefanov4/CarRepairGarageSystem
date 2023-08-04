namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CarRepairGarage.Services.Contracts;
    using Common;

    /// <summary>
    /// Controller responsible for handling home-related actions.
    /// </summary>
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IGarageService garageService;

        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger service.</param>
        /// <param name="service">The garage service.</param>
        public HomeController(ILogger<HomeController> logger, IGarageService service)
        {
            _logger = logger;
            garageService = service;
        }

        /// <summary>
        /// Displays the home page with a list of garages.
        /// </summary>
        /// <returns>The view containing the list of garages.</returns>
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(GeneralApplicationConstants.Roles.ManagerRole))
            {
                return RedirectToAction("All","Dashboard", new { Area = GeneralApplicationConstants.Roles.ManagerRole });
            }

            if (User.IsInRole(GeneralApplicationConstants.Roles.AdminRole))
            {
                return RedirectToAction("Index", "Dashboard", new { Area = GeneralApplicationConstants.Roles.AdminRole });
            }
            var model = await garageService.GetAllGaragesAsync(3);
            return View(model);
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>The view containing the privacy policy information.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Handles errors and displays the corresponding error view based on the status code.
        /// </summary>
        /// <param name="statusCode">The status code of the error.</param>
        /// <returns>The view containing the error information.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }

            return View();
        }
    }
}