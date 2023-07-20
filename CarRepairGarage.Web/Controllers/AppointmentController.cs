using CarRepairGarage.Data.Models;
using CarRepairGarage.Services;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static CarRepairGarage.Common.NotificationsMessagesConstants;

namespace CarRepairGarage.Web.Controllers
{
    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.UserRole)]
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IGarageService _garageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(
            IAppointmentService appointmentService,
            IGarageService garageService,
            UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _garageService = garageService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var model = await _appointmentService.GetAllAppointmentsByUserIdAsync(user.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            try
            {
                await _appointmentService.CreateAppointmentAsync(model, user);
                TempData[SuccessMessage] = $"Your Appointment was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(DateTime selectedDate, int garageId, int serviceId)
        {
            List<string> bookedHours = await _appointmentService.GetAllAvailableHours(selectedDate,garageId, serviceId);

            List<string> availableTimes = new List<string>()
            {
                "09:00", "10:00", "11:00"/*, "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"*/
            };

            availableTimes.RemoveAll(x => bookedHours.Contains(x));

            if (availableTimes.Count == 0)
            {
                availableTimes.Add("There are no any free hours for this Date!");
            }

            return new JsonResult(availableTimes);
        }

        [HttpGet]
        public async Task<IActionResult> GetGarageServices(int garageId)
        {
            var services = await _garageService.GetAllServicesByGarageIdAsync(garageId);
            
            string json = JsonConvert.SerializeObject(services, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return new ContentResult
            {
                Content = json,
                ContentType = "application/json"
            };
        }
    }
}
