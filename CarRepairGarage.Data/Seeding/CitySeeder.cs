namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    public class CitySeeder : ISeeder
    {
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
