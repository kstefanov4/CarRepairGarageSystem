﻿namespace CarRepairGarage.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CarRepairGarage.Data.Common.Helpers;
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for seeding the application database with initial Car data.
    /// </summary>
    public class CarSeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial Car data if the table is empty.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Guid userGuid = userManager.FindByNameAsync(CarRepairGarage.Common.GeneralApplicationConstants.AccountsData.UserEmail).Result.Id;

            if (dbContext.Cars.Any())
            {
                return;
            }

            Car[] cars = new Car[]
            {
                new Car
                {
                    VIN = "WBAFR7C51BC400485",
                    Make = "BMW",
                    Model = "535i",
                    Year = 2011,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = "WA1WMBFE1AD001465",
                    Make = "Audi",
                    Model = "Q7",
                    Year = 2010,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = "KNDUP131266609054",
                    Make = "Kia",
                    Model = "Sedona",
                    Year = 2006,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = "ZFF76ZFA0E0015561",
                    Make = "Ferrari",
                    Model = "Laferrari",
                    Year = 2014,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = "1FTCR10U4PPB51355",
                    Make = "FORD",
                    Model = "Ranger",
                    Year = 1993,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "NoReal",
                    Model = "Car",
                    Year = 2006,
                    IsDeleted = false,
                    UserId = userGuid
                }
            };
            await dbContext.Cars.AddRangeAsync(cars);
            await dbContext.SaveChangesAsync();
        }
    }
}
