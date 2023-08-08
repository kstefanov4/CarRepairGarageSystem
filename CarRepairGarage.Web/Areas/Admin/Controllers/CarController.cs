namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller for managing cars in the admin area.
    /// </summary>
    public class CarController : BaseController
    {
        private readonly ICarService _carService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarController"/> class.
        /// </summary>
        /// <param name="carService">The car service instance to be used.</param>
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Displays a list of all cars.
        /// </summary>
        /// <returns>The view containing the list of cars.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _carService.GetAllCarsAsync();
            return View(model);
        }

        /// <summary>
        /// Deletes a car by its ID.
        /// </summary>
        /// <param name="id">The ID of the car to be deleted.</param>
        /// <returns>
        /// - If the car exists, it will be deleted, and the user will be redirected to the Index page.
        /// - If the car does not exist, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// - If any exception occurs during the deletion process, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// </returns>
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
