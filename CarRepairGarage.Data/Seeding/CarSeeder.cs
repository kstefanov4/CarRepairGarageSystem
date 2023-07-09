namespace CarRepairGarage.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    
    using CarRepairGarage.Data.Common.Helpers;
    using CarRepairGarage.Data.Models;

    public class CarSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Guid userGuid = userManager.FindByNameAsync("user@mail.com").Result.Id;

            if (dbContext.Cars.Any())
            {
                return;
            }

            Car[] cars = new Car[]
            {
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "Opel",
                    Model = "Zafira",
                    Year = 2013,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "Audi",
                    Model = "A4",
                    Year = 2010,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "Mercedes",
                    Model = "ML",
                    Year = 2016,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "BMW",
                    Model = "M3",
                    Year = 2011,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "Toyota",
                    Model = "Yaris",
                    Year = 2017,
                    IsDeleted = false,
                    UserId = userGuid
                },
                new Car
                {
                    VIN = GenerateRandomVin.Generate(),
                    Make = "Volvo",
                    Model = "XC90",
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
