namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Service;
    using Microsoft.AspNetCore.Mvc;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller for managing services in the admin area.
    /// </summary>
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceController"/> class.
        /// </summary>
        /// <param name="serviceService">The service service instance to be used.</param>
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// Displays a list of all services.
        /// </summary>
        /// <returns>The view containing the list of services.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _serviceService.GetAllServiceAsync();
            return View(model);
        }

        /// <summary>
        /// Displays the form for adding a new service.
        /// </summary>
        /// <returns>The view containing the form for adding a new service.</returns>
        [HttpGet]
        public IActionResult AddService()
        {
            var model = new AddServiceViewModel();
            return View(model);
        }

        /// <summary>
        /// Handles the HTTP POST request to add a new service.
        /// </summary>
        /// <param name="model">The model containing the data for the new service.</param>
        /// <returns>
        /// - If the model is valid, the new service will be added, and the user will be redirected to the Index page with a success message.
        /// - If the model is invalid, the user will be shown the AddService view again with validation errors.
        /// - If any exception occurs during the service creation process, an error message will be set in TempData, and the user will be redirected to the AddService view.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _serviceService.AddAsync(model);
                TempData[SuccessMessage] = $"Your service {model.Name} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a service by its ID.
        /// </summary>
        /// <param name="id">The ID of the service to be deleted.</param>
        /// <returns>
        /// - If the service exists, it will be deleted, and the user will be redirected to the Index page with a success message.
        /// - If the service does not exist, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// - If any exception occurs during the deletion process, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// </returns>
        public async Task<IActionResult> Delete(int id)
        {
            if ((await _serviceService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This Service does not exist!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _serviceService.DeleteAsync(id);
                TempData[SuccessMessage] = $"Your Service was successfully deleted.";
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