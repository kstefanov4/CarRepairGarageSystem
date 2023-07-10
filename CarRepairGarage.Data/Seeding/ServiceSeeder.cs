namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    public class ServiceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Services.Any())
            {
                return;
            }

            Service[] services = new Service[]
            {
                new Service
                {
                    Name = "Oil change and filter replacement",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Serpentine belt inspection",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Wiper blade inspection",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Tire pressure checks",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Engine air filter",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Cabin air filter",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Coolant",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Belts and hoses",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Brake pads",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Brake fluid exchange",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Spark plug replacement",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Transmission fluid inspection",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Timing belt replacement",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Battery testing",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Tire replacement",
                    IsDeleted = false
                }
            };
            await dbContext.Services.AddRangeAsync(services);
            await dbContext.SaveChangesAsync();
        }
    }
}
