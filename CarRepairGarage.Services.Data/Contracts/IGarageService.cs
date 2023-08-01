namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Web.ViewModels.Note;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to garages in the car repair garage application.
    /// </summary>
    public interface IGarageService
    {
        /// <summary>
        /// Retrieves a collection of garage view models representing all available garages.
        /// </summary>
        /// <param name="count">The maximum number of garages to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation and returns a collection of garage view models.</returns>
        Task<IEnumerable<GarageViewModel>> GetAllGaragesAsync(int count);

        /// <summary>
        /// Retrieves a collection of garage services models representing all services offered by a specific garage.
        /// </summary>
        /// <param name="garageId">The ID of the garage.</param>
        /// <returns>An asynchronous task that represents the operation and returns a collection of garage services models.</returns>
        Task<List<GarageServicesModel>> GetAllServicesByGarageIdAsync(int garageId);

        /// <summary>
        /// Retrieves a collection of garage view models representing all available garages, filtered and paged based on the query model.
        /// </summary>
        /// <param name="queryModel">The query model containing filter and paging information.</param>
        /// <returns>An asynchronous task that represents the operation and returns a collection of filtered and paged garage view models.</returns>
        Task<AllGaragesFilteredAndPagedServiceModel> AllAsync(AllGaragesQueryModel queryModel);

        /// <summary>
        /// Retrieves the garage view model with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the garage to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation and returns the garage view model with the specified ID.</returns>
        Task<GarageViewModel> GetGarageByIdAsync(int id);

        /// <summary>
        /// Retrieves the modify garage view model with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the garage to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation and returns the modify garage view model with the specified ID.</returns>
        Task<ModifyGarageViewModel> ModifyGarageByIdAsync(int id);

        /// <summary>
        /// Retrieves a collection of garage view models representing all garages owned by a specific user.
        /// </summary>
        /// <param name="id">The ID of the user who owns the garages.</param>
        /// <returns>An asynchronous task that represents the operation and returns a collection of garage view models.</returns>
        Task<IEnumerable<GarageViewModel>> GetAllGaragesByOwnerAsync(string id);

        /// <summary>
        /// Adds a new garage to the database based on the provided model and user.
        /// </summary>
        /// <param name="model">The add garage view model containing the details of the new garage.</param>
        /// <param name="user">The user who owns the garage.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task AddGarageAsync(AddGarageViewModel model, ApplicationUser user);

        /// <summary>
        /// Checks if a garage with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the garage to check for existence.</param>
        /// <returns>An asynchronous task that represents the operation and returns true if the garage exists, otherwise false.</returns>
        Task<bool> Exists(int id);

        /// <summary>
        /// Updates an existing garage in the database based on the provided modify garage view model.
        /// </summary>
        /// <param name="model">The modify garage view model containing the updated details of the garage.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task Edit(ModifyGarageViewModel model);

        /// <summary>
        /// Deletes the garage with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the garage to delete.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task Delete(int id);
    }

}
