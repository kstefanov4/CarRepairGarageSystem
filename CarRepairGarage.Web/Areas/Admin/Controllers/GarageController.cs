namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller for managing garages in the admin area.
    /// </summary>
    public class GarageController : BaseController
    {
        private readonly IGarageService _garageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GarageController"/> class.
        /// </summary>
        /// <param name="garageService">The garage service instance to be used.</param>
        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        /// <summary>
        /// Displays a list of all garages.
        /// </summary>
        /// <returns>The view containing the list of garages.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _garageService.GetAllGaragesAsync(int.MaxValue);
            return View(model);
        }

        /// <summary>
        /// Deletes a garage by its ID.
        /// </summary>
        /// <param name="id">The ID of the garage to be deleted.</param>
        /// <returns>
        /// - If the garage exists, it will be deleted, and the user will be redirected to the Index page with a success message.
        /// - If the garage does not exist, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// - If any exception occurs during the deletion process, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// </returns>
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
