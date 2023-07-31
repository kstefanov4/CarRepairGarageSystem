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

    public class CarService : BaseService, ICarService
    {
        private readonly IRepository _repository;

        public CarService(
            IRepository repository,
            ILogger<CarService> logger) : base(logger)
        {
            _repository = repository;
        }

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

        public async Task<bool> Exist(int id)
        {
            return await _repository.AllReadonly<Car>()
                .AnyAsync(x => x.Id == id);
        }

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
                }).ToListAsync();

            return cars;
        }

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
