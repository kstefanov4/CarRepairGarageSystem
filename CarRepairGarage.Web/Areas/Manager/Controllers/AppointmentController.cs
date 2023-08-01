namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Appointment;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller for managing appointment-related operations in the Manager area.
    /// </summary>
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IGarageService _garageService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service.</param>
        /// <param name="garageService">The garage service.</param>
        /// <param name="userManager">The user manager.</param>
        public AppointmentController(
            IAppointmentService appointmentService,
            IGarageService garageService,
            UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _garageService = garageService;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays all appointments associated with the current garage.
        /// </summary>
        /// <param name="queryModel">The query model containing filters and pagination details.</param>
        /// <returns>The view displaying all appointments for the current garage.</returns>
        public async Task<IActionResult> All([FromQuery] AllAppointmentsQueryModel queryModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            
            AllAppointmentsFilteredAndPagedServiceModel serviceModel = await _appointmentService.GetAllAppointmentsByGarageIdAsync(queryModel, user.Id);

            queryModel.Appointments = serviceModel.Appointments;

            return View(queryModel);
        }

        /// <summary>
        /// Approves an appointment with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to approve.</param>
        /// <returns>A redirect to the view displaying all appointments for the current garage.</returns>
        public async Task<IActionResult> Approve(Guid id)
        {

            if ((await _appointmentService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This appointment does not exist!";
                return RedirectToAction(nameof(All));
            }

            var user = await _userManager.GetUserAsync(User);
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id.ToString());


            if (appointment.GarageOwner != user.Id.ToString())
            {
                TempData[ErrorMessage] = "You are not the owner of this car. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            try
            {
                await _appointmentService.Approve(id.ToString());
                TempData[SuccessMessage] = $"The Appointment was successfully Approved.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Rejects an appointment with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to reject.</param>
        /// <returns>A redirect to the view displaying all appointments for the current garage.</returns>
        public async Task<IActionResult> Reject(Guid id)
        {
            if ((await _appointmentService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This appointment does not exist!";
                return RedirectToAction(nameof(All));
            }

            var user = await _userManager.GetUserAsync(User);
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id.ToString());


            if (appointment.GarageOwner != user.Id.ToString())
            {
                TempData[ErrorMessage] = "You are not the owner of this car. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            try
            {
                await _appointmentService.Reject(id.ToString());
                TempData[SuccessMessage] = $"The Appointment was successfully Rejected.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(All));
        }
    }
}
