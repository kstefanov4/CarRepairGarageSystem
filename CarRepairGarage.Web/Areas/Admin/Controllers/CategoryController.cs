namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Category;
    using static CarRepairGarage.Common.NotificationsMessagesConstants;
    using CarRepairGarage.Services;

    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _categoryService.GetAllCategoryAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new AddCategoryViewModel();
            return View(model);
        }

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
