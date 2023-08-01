namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using CarRepairGarage.Data;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for orchestrating the seeding process for the application database.
    /// </summary>
    public class Seeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial data for roles, accounts, notes, cars, cities, addresses, categories, services, garages, garage services, and appointments.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="dbContext"/> or <paramref name="serviceProvider"/> is null.</exception>
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

            // Seed data for each individual entity/table using their respective seeders
            await new RoleSeeder().SeedAsync(dbContext, serviceProvider);
            await new AccountSeeder().SeedAsync(dbContext, serviceProvider);
            await new NoteSeeder().SeedAsync(dbContext, serviceProvider);
            await new CarSeeder().SeedAsync(dbContext, serviceProvider);
            await new CitySeeder().SeedAsync(dbContext, serviceProvider);
            await new AddressSeeder().SeedAsync(dbContext, serviceProvider);
            await new CategorySeeder().SeedAsync(dbContext, serviceProvider);
            await new ServiceSeeder().SeedAsync(dbContext, serviceProvider);
            await new GarageSeeder().SeedAsync(dbContext, serviceProvider);
            await new GarageServiceSeeder().SeedAsync(dbContext, serviceProvider);
            await new AppointmentSeeder().SeedAsync(dbContext, serviceProvider);
            
        }
    }
}
