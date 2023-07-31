namespace CarRepairGarage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRepairGarage.Services.Contracts;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await categoryService.GetAllCategoryAsync();
            return View(model);
        }


    }
}
