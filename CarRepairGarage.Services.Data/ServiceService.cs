namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Web.ViewModels.Service;

    /// <summary>
    /// Service class for managing service-related operations.
    /// </summary>
    public class ServiceService : IServiceService
    {
        protected readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        public ServiceService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves the names of all services asynchronously.
        /// </summary>
        /// <returns>A collection of service names.</returns>
        public async Task<IEnumerable<string>> AllServicesNameAsync()
        {
            return await _repository.AllReadonly<Service>()
                .Select(x => x.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all services asynchronously.
        /// </summary>
        /// <returns>A list of <see cref="ServiceViewModel"/> containing service details.</returns>
        public async Task<List<ServiceViewModel>> GetAllServiceAsync()
        {
            var services = await _repository.AllReadonly<Service>()
                                    .Where(x => x.IsDeleted == false)
                                    .Include(x => x.Garages)
                                    .Select(x => new ServiceViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Description = x.Description,
                                        Garages = x.Garages.Select(g => new GarageViewModel
                                        {
                                            Id = g.Garage.Id,
                                            Name = g.Garage.Name
                                        }).ToList()
                                    }).ToListAsync();
            return services;
        }

        /// <summary>
        /// Retrieves the name of the service with the specified ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the service to retrieve.</param>
        /// <returns>The name of the service.</returns>
        public async Task<string> GetServiceByIdAsync(int id)
        {
            return await _repository.AllReadonly<Service>()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(x => x.Name)
                .FirstAsync();
        }
    }
}
