namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Newtonsoft.Json;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Appointment;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    /// <summary>
    /// Controller responsible for managing user appointments.
    /// </summary>
    [Authorize(Roles = Common.GeneralApplicationConstants.Roles.UserRole)]
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
        /// Displays the appointment creation form.
        /// </summary>
        /// <param name="id">The ID of the garage for which to create an appointment (optional).</param>
        /// <returns>The view for creating an appointment.</returns>
        [HttpGet]
        public IActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                return View();

            }

            CreateAppointmentModel model = new CreateAppointmentModel()
            {
                GarageId = id,
                SelectedDate = DateTime.Now,
            };
            return View(model);
        }

        /// <summary>
        /// Displays the appointment reservation form.
        /// </summary>
        /// <param name="Id">The ID of the garage for which to reserve an appointment.</param>
        /// <returns>The view for reserving an appointment.</returns>
        [HttpGet]
        public IActionResult Reserve(int Id)
        {
            CreateAppointmentModel model = new CreateAppointmentModel()
            {
                GarageId = Id
            };
            return View(model);
        }

        /// <summary>
        /// Retrieves all appointments for the current user.
        /// </summary>
        /// <param name="queryModel">The query parameters for filtering and pagination.</param>
        /// <returns>The view displaying all appointments for the current user.</returns>
        public async Task<IActionResult> All([FromQuery] AllAppointmentsQueryModel queryModel)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            AllAppointmentsFilteredAndPagedServiceModel serviceModel = await _appointmentService.GetAllAppointmentsByUserIdAsync(queryModel, user.Id);

            queryModel.Appointments = serviceModel.Appointments;

            return View(queryModel);
        }

        /// <summary>
        /// Removes an appointment for the current user.
        /// </summary>
        /// <param name="id">The ID of the appointment to be removed.</param>
        /// <returns>The view displaying all appointments after the removal.</returns>
        public async Task<IActionResult> Remove(Guid id)
        {
            if ((await _appointmentService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This appointment does not exist!";
                return RedirectToAction(nameof(All));
            }

            var user = await _userManager.GetUserAsync(User);
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id.ToString());


            if (appointment.UserId != user.Id)
            {
                TempData[ErrorMessage] = "You are not the owner of this car. Please contact our support team!";
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            try
            {
                await _appointmentService.Delete(id);
                TempData[SuccessMessage] = $"Your appointment was successfully deleted.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction(nameof(All));
            }


            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Creates a new appointment for the current user.
        /// </summary>
        /// <param name="model">The model containing the appointment details.</param>
        /// <returns>The view displaying all appointments after the creation.</returns>
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

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Retrieves the available times for a selected date, garage, and service.
        /// </summary>
        /// <param name="selectedDate">The selected date for the appointment.</param>
        /// <param name="garageId">The ID of the selected garage.</param>
        /// <param name="serviceId">The ID of the selected service.</param>
        /// <returns>A JSON result containing the available times.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(DateTime selectedDate, int garageId, int serviceId)
        {
            List<string> bookedHours = await _appointmentService.GetAllAvailableHours(selectedDate, garageId, serviceId);

            List<string> availableTimes = new List<string>()
            {
                "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"
            };

            availableTimes.RemoveAll(x => bookedHours.Contains(x));

            if (availableTimes.Count == 0)
            {
                availableTimes.Add("There are no any free hours for this Date!");
            }

            return new JsonResult(availableTimes);
        }

        /// <summary>
        /// Retrieves the services offered by a specific garage.
        /// </summary>
        /// <param name="garageId">The ID of the garage.</param>
        /// <returns>A JSON result containing the services offered by the garage.</returns>
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
