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
        public IActionResult Index(int id = 0)
        {
            if (id == 0 )
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

        [HttpGet]
        public IActionResult Reserve(int Id)
        {
            CreateAppointmentModel model = new CreateAppointmentModel()
            {
                GarageId = Id
            };
            return View(model);
        }

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

            /*var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var model = await _appointmentService.GetAllAppointmentsByUserIdAsync(user.Id);
            return View(model);*/
        }

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

        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(DateTime selectedDate, int garageId, int serviceId)
        {
            List<string> bookedHours = await _appointmentService.GetAllAvailableHours(selectedDate,garageId, serviceId);

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
