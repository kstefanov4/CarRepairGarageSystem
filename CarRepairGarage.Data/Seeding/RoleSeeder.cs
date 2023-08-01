namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for seeding the application database with initial Role data.
    /// </summary>
    internal class RoleSeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial Role data for Admin, Manager, and User roles.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(dbContext, roleManager, GeneralApplicationConstants.Roles.AdminRole);
            await SeedRoleAsync(dbContext, roleManager, GeneralApplicationConstants.Roles.ManagerRole);
            await SeedRoleAsync(dbContext, roleManager, GeneralApplicationConstants.Roles.UserRole);
        }

        private static async Task SeedRoleAsync(ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
