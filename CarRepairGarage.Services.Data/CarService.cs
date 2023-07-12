namespace CarRepairGarage.Services
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Car;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Security.AccessControl;
    using System.Threading.Tasks;

    public class CarService : ICarService
    {
        private readonly IRepository repository;

        public CarService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<CarViewModel>> GetAllCarsByIdAsync(Guid id)
        {
            var cars = await repository.AllReadonly<Car>()
                .Where(x => x.IsDeleted == false && x.User.Id == id)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    VIN = x.VIN,
                    Make = x.Make,
                    Model = x.Model,
                    Year = x.Year,
                }).ToListAsync();

            return cars;
        }
    }
}
