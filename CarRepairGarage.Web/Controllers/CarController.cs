using CarRepairGarage.Data.Models;
using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService carService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarController(
            ICarService carService, 
            UserManager<ApplicationUser> userManager)
        {
            this.carService = carService; 
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var model = await carService.GetAllCarsByIdAsync(user.Id);
            return View(model);
        }
    }
}
