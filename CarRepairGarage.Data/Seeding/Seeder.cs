namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using CarRepairGarage.Data;
    using CarRepairGarage.Data.Seeding.Contracts;

    public class Seeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            await new RoleSeeder().SeedAsync(dbContext, serviceProvider);
            await new AccountSeeder().SeedAsync(dbContext, serviceProvider);
            await new CarSeeder().SeedAsync(dbContext, serviceProvider);
            await new CitySeeder().SeedAsync(dbContext, serviceProvider);
            await new AddressSeeder().SeedAsync(dbContext, serviceProvider);
            await new CategorySeeder().SeedAsync(dbContext, serviceProvider);
            await new ServiceSeeder().SeedAsync(dbContext, serviceProvider);
            await new GarageSeeder().SeedAsync(dbContext, serviceProvider);
            await new GarageServiceSeeder().SeedAsync(dbContext, serviceProvider);
            await new AppointmentSeeder().SeedAsync(dbContext, serviceProvider);
            await new NoteSeeder().SeedAsync(dbContext, serviceProvider);
            
        }
    }
}
