namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CarRepairGarage.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(dbContext, roleManager, "Admin");
            await SeedRoleAsync(dbContext, roleManager, "Manager");
            await SeedRoleAsync(dbContext, roleManager, "Supervisor");
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
