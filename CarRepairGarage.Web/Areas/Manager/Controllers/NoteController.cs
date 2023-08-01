namespace CarRepairGarage.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Note;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;


    /// <summary>
    /// Controller for managing notes in the manager's dashboard.
    /// </summary>
    public class NoteController : BaseController
    {
        private readonly INoteService _noteService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteController"/> class.
        /// </summary>
        /// <param name="noteService">The note service.</param>
        /// <param name="userManager">The user manager.</param>
        public NoteController(
            INoteService noteService,
            UserManager<ApplicationUser> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        /// <summary>
        /// Action to display the form for adding a new note.
        /// </summary>
        /// <returns>The view with the form for adding a new note.</returns>
        public IActionResult Index()
        {
            var model = new AddNoteViewModel();
            return View(model);
        }

        /// <summary>
        /// Action to handle the new note form submission.
        /// </summary>
        /// <param name="model">The view model containing new note data.</param>
        /// <returns>The view with the result of the note addition attempt.</returns>
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

            return RedirectToAction("All", "Dashboard");
        }

        /// <summary>
        /// Action to handle note removal.
        /// </summary>
        /// <param name="id">The ID of the note to remove.</param>
        /// <returns>The view with the result of the note removal attempt.</returns>
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

        /// <summary>
        /// Action to handle removal of a note from all associated garages.
        /// </summary>
        /// <param name="id">The ID of the note to remove from all garages.</param>
        /// <returns>The view with the result of the note removal attempt.</returns>
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
