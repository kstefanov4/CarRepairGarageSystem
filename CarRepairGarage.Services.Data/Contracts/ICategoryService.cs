namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Category;
    
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoryAsync();
    }
}
