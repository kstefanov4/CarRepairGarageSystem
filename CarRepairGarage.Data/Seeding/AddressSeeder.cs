namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for seeding the application database with initial Address data.
    /// </summary>
    public class AddressSeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial Address data if the table is empty.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Addresses.Any())
            {
                return;
            }

            Address[] addreses = new Address[]
            {
                new Address
                {
                    CityId = 1,
                    StreetName = "Bul. Bulgaria",
                    StreetNumber = 69,
                    IsDeleted = false
                },
                new Address
                {
                    CityId = 2,
                    StreetName = "ul. Vazov",
                    StreetNumber = 18,
                    IsDeleted = false
                },
                new Address
                {
                    CityId = 3,
                    StreetName = "ul. Chervenite Borove",
                    StreetNumber = 8,
                    IsDeleted = false
                },
                new Address
                {
                    CityId = 4,
                    StreetName = "ul. Trakia",
                    StreetNumber = 4,
                    IsDeleted = false
                }
            };

            await dbContext.Addresses.AddRangeAsync(addreses);
            await dbContext.SaveChangesAsync();
        }
    }
}
