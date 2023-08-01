namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for seeding the application database with initial City data.
    /// </summary>
    public class CitySeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial City data if the table is empty.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            City[] cities = new City[]
            {
                new City
                {
                    
                    Name = "Sofia",
                    IsDeleted = false
                },
                new City
                {
                    
                    Name = "Plovdiv",
                    IsDeleted = false
                },
                new City
                {
                    
                    Name = "Varna",
                    IsDeleted = false
                },
                new City
                {
                    
                    Name = "Burgas",
                    IsDeleted = false
                }
            };
            await dbContext.Cities.AddRangeAsync(cities);
            await dbContext.SaveChangesAsync();
        }
    }
}
