namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;
    using CarRepairGarage.Common;

    /// <summary>
    /// Class responsible for seeding the application database with initial user accounts.
    /// </summary>
    public class AccountSeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial user accounts.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Create Admin
            await CreateUser(
                dbContext,
                userManager,
                roleManager,
                GeneralApplicationConstants.AccountsData.AdminEmail,
                GeneralApplicationConstants.Roles.AdminRole,
                "Admincho",
                "Adminov");

            // Create User
            await CreateUser(
                dbContext,
                userManager,
                roleManager,
                GeneralApplicationConstants.AccountsData.UserEmail,
                GeneralApplicationConstants.Roles.UserRole,
                "Usercho",
                "Userov");

            // Create Manager
            await CreateUser(
                dbContext,
                userManager,
                roleManager,
                GeneralApplicationConstants.AccountsData.ManagerEmail,
                GeneralApplicationConstants.Roles.ManagerRole,
                "Garage",
                "Mladost");
        }

        private static async Task CreateUser(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            string email,
            string roleName = null,
            string firstName = null,
            string lastName = null)
        {

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var password = "123456";

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                bool exist = userManager.Users.Any(x => x.Roles.Any(x => x.RoleId.ToString() == role.Id.ToString()));

                if (!exist)
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);

                        if (firstName != null || lastName != null)
                        {
                            await userManager.AddClaimAsync(user, new Claim("FirstName", firstName!));
                            await userManager.AddClaimAsync(user, new Claim("LastName", lastName));
                        }
                    }

                }
            }
            else
            {

                var result = await userManager.CreateAsync(user, password);

            }

            await dbContext.SaveChangesAsync();
        }
    }
}
