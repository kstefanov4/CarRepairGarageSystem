﻿namespace CarRepairGarage.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using CarRepairGarage.Data;

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

            await new RolesSeeder().SeedAsync(dbContext, serviceProvider);
            await new AccountsSeeder().SeedAsync(dbContext, serviceProvider);
            
        }
    }
}
