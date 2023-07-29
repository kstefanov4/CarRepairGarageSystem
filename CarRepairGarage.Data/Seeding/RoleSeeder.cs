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

    internal class RoleSeeder : ISeeder
    {
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
