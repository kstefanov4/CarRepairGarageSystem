using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Note;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static CarRepairGarage.Common.NotificationsMessagesConstants;

namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    public class NoteController : BaseController
    {
        private readonly IGarageService _garageService;

        public NoteController(IGarageService garageService)
        {
            _garageService = garageService;
        }
        public IActionResult Index()
        {
            var model = new AddNoteViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddNoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _garageService.CreateNoteAsync(model);
                TempData[SuccessMessage] = $"Your Note {model.Title} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("All","Dashboard");
        }
    }
}
