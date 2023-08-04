namespace CarRepairGarage.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Car;

    /// <summary>
    /// Service class for managing car-related operations.
    /// </summary>
    public class CarService : BaseService, ICarService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        /// <param name="logger">The logger used for logging messages.</param>
        public CarService(
            IRepository repository,
            ILogger<CarService> logger) : base(logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adds a new car to the database.
        /// </summary>
        /// <param name="carModel">The car information to add as an <see cref="AddCarViewModel"/>.</param>
        /// <param name="user">The application user associated with the car.</param>
        public async Task AddCarAsync(AddCarViewModel carModel, ApplicationUser user)
        {
            Car car = new Car()
            {
                VIN = carModel.VIN,
                Make = carModel.Make,
                Model = carModel.CarModel,
                Year = carModel.Year,
                User = user
            };

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.AddAsync(car);
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Deletes a car from the database.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        public async Task Delete(int id)
        {
            var car = await _repository.GetByIdAsync<Car>(id);
            car.IsDeleted = true;
            car.DeletedOn = DateTime.Now;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Updates the information of an existing car in the database.
        /// </summary>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="model">The updated car information as an <see cref="AddCarViewModel"/>.</param>
        public async Task Edit(int id, AddCarViewModel model)
        {
            Car car = await _repository.GetByIdAsync<Car>(id);
            
            car.VIN = model.VIN;
            car.Make = model.Make;
            car.Model = model.CarModel;
            car.Year = model.Year;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Checks if a car with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the car to check.</param>
        /// <returns>True if the car exists; otherwise, false.</returns>
        public async Task<bool> Exist(int id)
        {
            return await _repository.AllReadonly<Car>()
                .AnyAsync(x => x.Id == id);
        }

        /// <summary>
        /// Retrieves all cars associated with a specific user.
        /// </summary>
        /// <param name="id">The ID of the user to filter the cars.</param>
        /// <returns>A collection of <see cref="CarViewModel"/> representing the cars.</returns>
        public async Task<IEnumerable<CarViewModel>> GetAllCarsByUserIdAsync(Guid id)
        {
            var cars = await _repository.AllReadonly<Car>()
                .Where(x => x.IsDeleted == false && x.User.Id == id)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    VIN = x.VIN,
                    Make = x.Make,
                    CarModel = x.Model,
                    Year = x.Year,
                    UserId = x.UserId.ToString()
                }).ToListAsync();

            return cars;
        }


        /// <summary>
        /// Retrieves all cars
        /// </summary>
        /// <returns>A collection of <see cref="CarViewModel"/> representing the cars.</returns>
        public async Task<IEnumerable<CarViewModel>> GetAllCarsAsync()
        {
            var cars = await _repository.AllReadonly<Car>()
                .Where(x => x.IsDeleted == false)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    VIN = x.VIN,
                    Make = x.Make,
                    CarModel = x.Model,
                    Year = x.Year,
                    UserId = x.UserId.ToString()
                }).ToListAsync();

            return cars;
        }

        /// <summary>
        /// Retrieves the car with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>A <see cref="CarViewModel"/> representing the car.</returns>
        public async Task<CarViewModel> GetCarByIdAsync(int id)
        {
            var car = await _repository.GetByIdAsync<Car>(id);

            return new CarViewModel()
            {
                Id = car.Id,
                VIN = car.VIN,
                Make = car.Make,
                CarModel = car.Model,
                Year = car.Year,
                UserId = car.UserId.ToString()
            };
        }
    }
}
