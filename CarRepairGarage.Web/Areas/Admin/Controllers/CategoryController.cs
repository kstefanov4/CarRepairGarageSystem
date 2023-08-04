using CarRepairGarage.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairGarage.Web.Areas.Admin.Controllers
{
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
    }
}
