namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Service;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to services in the car repair garage application.
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Retrieves a list of all services available in the database.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation and returns a list of service view models.</returns>
        Task<List<ServiceViewModel>> GetAllServiceAsync();

        /// <summary>
        /// Retrieves the names of all services available in the database.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation and returns a collection of service names.</returns>
        Task<IEnumerable<string>> AllServicesNameAsync();

        /// <summary>
        /// Retrieves the name of the service with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the service.</param>
        /// <returns>An asynchronous task that represents the operation and returns the name of the service as a string.</returns>
        Task<string> GetServiceByIdAsync(int id);
        Task AddAsync(AddServiceViewModel model);

        Task DeleteAsync(int id);
        Task<bool> Exist(int id);
    }

}
