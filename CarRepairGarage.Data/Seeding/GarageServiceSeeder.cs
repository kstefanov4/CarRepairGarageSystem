namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for seeding the application database with initial GarageService data.
    /// </summary>
    public class GarageServiceSeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial GarageService data if the table is empty.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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
