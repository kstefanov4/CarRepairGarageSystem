using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
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
    }
}
