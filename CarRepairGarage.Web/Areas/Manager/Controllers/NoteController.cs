namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Note;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;


    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public NoteController(
            INoteService noteService,
            UserManager<ApplicationUser> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
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
                await _noteService.CreateNoteAsync(model);
                TempData[SuccessMessage] = $"Your Note {model.Title} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("All","Dashboard");
        }

        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _noteService.Delete(id);
                TempData[SuccessMessage] = $"Your Note was successfully deleted.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction("All", "Dashboard");
            }

            return RedirectToAction("All", "Dashboard");
        }
        public async Task<IActionResult> RemoveAll(int id)
        {
            try
            {
                await _noteService.DeleteAll(id);
                TempData[SuccessMessage] = $"Your Note was successfully deleted.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return RedirectToAction("All", "Dashboard");
            }

            return RedirectToAction("All", "Dashboard");
        }
    }
}
