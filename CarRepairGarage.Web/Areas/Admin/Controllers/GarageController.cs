namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;
    public class GarageController : BaseController
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _garageService.GetAllGaragesAsync(int.MaxValue);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if ((await _garageService.Exist(id) == false))
            {
                TempData[ErrorMessage] = "This Garage does not exist!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _garageService.Delete(id);
                TempData[SuccessMessage] = $"Your Garage was successfully deleted.";
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
