namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;
    public class CarController : BaseController
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _carService.GetAllCarsAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if ((await _carService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This Car does not exist!";
                return RedirectToAction(nameof(Index));
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
