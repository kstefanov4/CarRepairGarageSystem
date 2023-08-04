namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;

    /// <summary>
    /// Controller responsible for handling category-related actions.
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /// <summary>
        /// Displays the list of all categories.
        /// </summary>
        /// <returns>The view containing the list of categories.</returns>
        public async Task<IActionResult> Index()
        {
            var model = await _categoryService.GetAllCategoryAsync();
            return View(model);
        }
    }
}
