namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller for managing users in the admin area.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service instance to be used.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Displays a list of all users.
        /// </summary>
        /// <returns>The view containing the list of users.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetAllUsersAsync();
            return View(model);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>
        /// - If the user exists, they will be deleted, and the user will be redirected to the Index page with a success message.
        /// - If the user does not exist, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// - If any exception occurs during the deletion process, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// </returns>
        public async Task<IActionResult> Delete(Guid id)
        {
            if ((await _userService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This User does not exist!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _userService.DeleteAsync(id);
                TempData[SuccessMessage] = $"This User was successfully deleted.";
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
