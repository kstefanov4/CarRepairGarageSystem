namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _appointmentService.GetAllAppointmentsAsync();
            return View(model);
        }

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
