namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Category;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;
    using CarRepairGarage.Services;

    /// <summary>
    /// Controller for managing categories in the admin area.
    /// </summary>
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryService">The category service instance to be used.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Displays a list of all categories.
        /// </summary>
        /// <returns>The view containing the list of categories.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _categoryService.GetAllCategoryAsync();
            return View(model);
        }

        /// <summary>
        /// Displays the form for adding a new category.
        /// </summary>
        /// <returns>The view containing the form for adding a new category.</returns>
        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new AddCategoryViewModel();
            return View(model);
        }

        /// <summary>
        /// Handles the HTTP POST request to add a new category.
        /// </summary>
        /// <param name="model">The model containing the data for the new category.</param>
        /// <returns>
        /// - If the model is valid, the new category will be added, and the user will be redirected to the Index page with a success message.
        /// - If the model is invalid, the user will be shown the AddCategory view again with validation errors.
        /// - If any exception occurs during the category creation process, an error message will be set in TempData, and the user will be redirected to the AddCategory view.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _categoryService.AddAsync(model);
                TempData[SuccessMessage] = $"Your Category {model.Name} was successfully created.";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = $"Something went wrong, please try once again or contact our support team!";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <returns>
        /// - If the category exists, it will be deleted, and the user will be redirected to the Index page with a success message.
        /// - If the category does not exist, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// - If any exception occurs during the deletion process, an error message will be set in TempData, and the user will be redirected to the Index page.
        /// </returns>
        public async Task<IActionResult> Delete(int id)
        {
            if ((await _categoryService.Exist(id)) == false)
            {
                TempData[ErrorMessage] = "This Category does not exist!";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _categoryService.DeleteAsync(id);
                TempData[SuccessMessage] = $"Your Category was successfully deleted.";
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
