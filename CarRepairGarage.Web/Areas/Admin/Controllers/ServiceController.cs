namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using CarRepairGarage.Services;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Service;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;

    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _serviceService.GetAllServiceAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            var model = new AddServiceViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _serviceService.AddServiceAsync(model);
                TempData[SuccessMessage] = $"Your service {model.Name} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if ((await _serviceService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This Service does not exist!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _serviceService.DeleteServiceAsync(id);
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
