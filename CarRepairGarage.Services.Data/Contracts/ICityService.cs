namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to cities in the car repair garage application.
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Retrieves a collection of names of all available cities.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation and returns a collection of city names.</returns>
        Task<IEnumerable<string>> AllCitiesNameAsync();

        /// <summary>
        /// Checks if a city with the specified name exists in the database.
        /// </summary>
        /// <param name="name">The name of the city to check for existence.</param>
        /// <returns>An asynchronous task that represents the operation and returns true if the city exists, otherwise false.</returns>
        Task<bool> Exist(string name);

        /// <summary>
        /// Retrieves the city with the specified name from the database.
        /// </summary>
        /// <param name="name">The name of the city to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation and returns the city with the specified name.</returns>
        Task<City> GetCityByNameAsync(string name);
    }

}
