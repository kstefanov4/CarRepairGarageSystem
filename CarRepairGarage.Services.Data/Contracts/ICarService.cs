namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Car;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to cars in the car repair garage application.
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Retrieves a collection of car view models belonging to the specified user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>An asynchronous task that represents the operation and returns a collection of car view models.</returns>
        Task<IEnumerable<CarViewModel>> GetAllCarsByUserIdAsync(Guid id);

        /// <summary>
        /// Retrieves a collection of car view models.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation and returns a collection of car view models.</returns>
        Task<IEnumerable<CarViewModel>> GetAllCarsAsync();

        /// <summary>
        /// Adds a new car to the user's collection of cars.
        /// </summary>
        /// <param name="carModel">The view model containing car information.</param>
        /// <param name="user">The application user who owns the car.</param>
        /// <returns>An asynchronous task representing the operation.</returns>
        Task AddCarAsync(AddCarViewModel carModel, ApplicationUser user);

        /// <summary>
        /// Checks if a car with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the car to check.</param>
        /// <returns>An asynchronous task that represents the operation and returns true if the car exists; otherwise, false.</returns>
        Task<bool> Exist(int id);

        /// <summary>
        /// Retrieves a car view model by its ID.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation and returns the car view model if found; otherwise, null.</returns>
        Task<CarViewModel> GetCarByIdAsync(int id);

        /// <summary>
        /// Edits the details of an existing car in the database.
        /// </summary>
        /// <param name="id">The ID of the car to edit.</param>
        /// <param name="model">The view model containing the updated car information.</param>
        /// <returns>An asynchronous task representing the operation.</returns>
        Task Edit(int id, AddCarViewModel model);

        /// <summary>
        /// Deletes a car with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>An asynchronous task representing the operation.</returns>
        Task Delete(int id);
    }

}
