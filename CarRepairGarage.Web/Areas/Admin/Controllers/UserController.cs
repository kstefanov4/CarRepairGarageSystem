namespace CarRepairGarage.Web.Areas.Admin.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _userService.GetAllUsersAsync();
            return View(model);
        }

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
