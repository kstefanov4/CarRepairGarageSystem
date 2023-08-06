namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Category;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to categories in the car repair garage application.
    /// </summary>
    public interface ICategoryService
    {

        /// <summary>
        /// Retrieves a collection of category view models representing all available categories.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation and returns a collection of category view models.</returns>
        Task<IEnumerable<CategoryViewModel>> GetAllCategoryAsync();

        /// <summary>
        /// Retrieves a collection of names of all available categories.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation and returns a collection of category names.</returns>
        Task<IEnumerable<string>> AllCategoriesNameAsync();

        /// <summary>
        /// Retrieves the name of the category with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve the name for.</param>
        /// <returns>An asynchronous task that represents the operation and returns the name of the category with the specified ID.</returns>
        Task<string> GetCategoryByIdAsync(int id);
        Task AddAsync(AddCategoryViewModel model);
        Task<bool> Exist(int id);
        Task DeleteAsync(int id);
    }

}
