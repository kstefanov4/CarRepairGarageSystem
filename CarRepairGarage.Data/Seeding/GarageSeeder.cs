﻿namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    
    public class GarageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Garages.Any())
            {
                return;
            }

            Garage[] garages = new Garage[]
            {
                new Garage
                {
                    Name = "Garage Sofia",
                    AddressId = 1,
                    CategoryId = 3,
                    ImageUrl = "https://media.istockphoto.com/id/513706427/photo/auto-repair-shop-with-car-serviced-by-mechanics.jpg?s=612x612&w=0&k=20&c=P8roDdc_ayEVEaGJlpwYf4QhxKJjEpJzeRtdxbGWiZc=",
                    IsDeleted = false
                },
                new Garage
                {
                    Name = "Garage Plovdiv",
                    AddressId = 2,
                    CategoryId = 1,
                    ImageUrl = "https://ringgitplus.com/en/blog/wp-content/uploads/2019/11/5bdad9364937c68168016781-e1635500569841.jpg",
                    IsDeleted = false
                },
                new Garage
                {
                    Name = "Garage Varna",
                    AddressId = 3,
                    CategoryId = 2,
                    ImageUrl = "https://www.carcility.com/blog/wp-content/uploads/2018/04/Car-Repair-and-Maintenance.jpg",
                    IsDeleted = false
                },
                new Garage
                {
                    Name = "Garage Burgas",
                    AddressId = 4,
                    CategoryId = 1,
                    ImageUrl = "https://gwrench.com/wp-content/uploads/2021/11/CarServiceVista.jpg",
                    IsDeleted = false
                }
            };
            await dbContext.Garages.AddRangeAsync(garages);
            await dbContext.SaveChangesAsync();
        }
    }
}