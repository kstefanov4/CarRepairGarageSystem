﻿namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;
    using System.Security.Claims;

    public class AccountSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Create Admin
            await CreateUser(
                dbContext,
                userManager,
                roleManager,
                "admin@mail.com",
                "Admin");

            // Create User
            await CreateUser(
                dbContext,
                userManager,
                roleManager,
                "user@mail.com",
                "User");

            await CreateUser(
                dbContext,
                userManager,
                roleManager,
                "user2@mail.com",
                "User",
                "User2",
                "Userov");
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

            var password = "Admin123!";

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
