namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    
    public class AddressSeeder : ISeeder
    {
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
                    StreetName = "ul. Kraimorska",
                    StreetNumber = 4,
                    IsDeleted = false
                }
            };

            await dbContext.Addresses.AddRangeAsync(addreses);
            await dbContext.SaveChangesAsync();
        }
    }
}
