namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Category;
    
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoryAsync();
        Task<IEnumerable<string>> AllCategoriesNameAsync();
        Task<string> GetCategoryByIdAsync(int id);
    }
}
