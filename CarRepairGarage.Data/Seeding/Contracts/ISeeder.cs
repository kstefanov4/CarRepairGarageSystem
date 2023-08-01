namespace CarRepairGarage.Data.Seeding.Contracts
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for database seeders. Implement this interface to define a seeder for the application database.
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial data.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
