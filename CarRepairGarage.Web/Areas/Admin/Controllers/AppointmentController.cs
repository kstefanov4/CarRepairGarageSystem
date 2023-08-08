namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller for managing appointments in the admin area.
    /// </summary>
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service instance to be used.</param>
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Displays a list of all appointments.
        /// </summary>
        /// <returns>The view containing the list of appointments.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _appointmentService.GetAllAppointmentsAsync();
            return View(model);
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to be deleted.</param>
        /// <returns>
        /// - If the appointment exists, it will be deleted and the user will be redirected to the Index page.
        /// - If the appointment does not exist, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// - If any exception occurs during the deletion process, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// </returns>
        public async Task<IActionResult> Delete(string id)
        {
            if ((await _appointmentService.Exist(Guid.Parse(id))) == false)
            {
                TempData[ErrorMessage] = "This Appointment does not exist!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _appointmentService.Delete(Guid.Parse(id));
                TempData[SuccessMessage] = $"Your Appointment was successfully deleted.";
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
