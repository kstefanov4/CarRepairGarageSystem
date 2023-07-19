using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Controllers
{
    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.UserRole)]
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IGarageService _garageService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IGarageService garageService)
        {
            _appointmentService = appointmentService;
            _garageService = garageService;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(DateTime selectedDate, int garageId)
        {
            List<string> bookedHours = await _appointmentService.GetAllAvailableHours(selectedDate,garageId);

            List<string> availableTimes = new List<string>()
            {
                "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"
            };

            availableTimes.RemoveAll(x => bookedHours.Contains(x));

            return new JsonResult(availableTimes);
        }

        [HttpGet]
        public async Task<IActionResult> GetGarageServices(int garageId)
        {
            var services = await _garageService.GetAllServicesByGarageIdAsync(garageId);

            return new JsonResult(services);
        }
    }
}
