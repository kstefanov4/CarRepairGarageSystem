namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;

    /// <summary>
    /// Service class for managing city-related operations.
    /// </summary>
    public class CityService : ICityService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        public CityService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves the names of all cities from the database.
        /// </summary>
        /// <returns>A collection of city names as strings.</returns>
        public async Task<IEnumerable<string>> AllCitiesNameAsync()
        {
            return await _repository.AllReadonly<City>()
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Checks if a city with the specified name exists in the database.
        /// </summary>
        /// <param name="name">The name of the city to check.</param>
        /// <returns>True if the city exists, false otherwise.</returns>
        public async Task<bool> Exist(string name)
        {
            return await _repository.AllReadonly<City>()
                .AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Retrieves the city with the specified name from the database.
        /// </summary>
        /// <param name="name">The name of the city to retrieve.</param>
        /// <returns>The <see cref="City"/> object representing the city.</returns>
        public async Task<City> GetCityByNameAsync(string name)
        {
            return await _repository.AllReadonly<City>()
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstAsync();
        }
    }

}
