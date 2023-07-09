namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
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
                    ServiceId = 1,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 2,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 3,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 1,
                    ServiceId = 4,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 5,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 6,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 7,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 2,
                    ServiceId = 8,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 9,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 10,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 11,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 3,
                    ServiceId = 12,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 4,
                    ServiceId = 13,
                    Available = true
                },
                new GarageService
                {
                    GarageId = 4,
                    ServiceId = 14,
                    Available = true
                },

                new GarageService
                {
                    GarageId = 4,
                    ServiceId = 15,
                    Available = true
                }
            };
            await dbContext.GaragesServices.AddRangeAsync(garageServices);
            await dbContext.SaveChangesAsync();
        }
    }
}
