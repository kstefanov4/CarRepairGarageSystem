namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    public class GarageServiceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GaragesServices.Any())
            {
                return;
            }

            GarageService[] garageServices = new GarageService[]
            {
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 1
                },
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 2
                },
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 3
                },
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 4
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 5
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 6
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 7
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 8
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 9
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 10
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 11
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 12
                },
                new GarageService
                {
                    GarageId = 4,
                    ServiceId = 13
                },
                new GarageService
                {
                    GarageId = 4,
                    ServiceId = 14
                },

                new GarageService
                {
                    GarageId = 4,
                    ServiceId = 15
                }
            };
            await dbContext.GaragesServices.AddRangeAsync(garageServices);
            await dbContext.SaveChangesAsync();
        }
    }
}
