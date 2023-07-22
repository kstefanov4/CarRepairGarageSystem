namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    public class ServiceSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Services.Any())
            {
                return;
            }

            Service[] services = new Service[]
            {
                new Service
                {
                    Name = "Oil change and filter replacement",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Serpentine belt inspection",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Wiper blade inspection",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Tire pressure checks",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Engine air filter",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Cabin air filter",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Coolant",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Belts and hoses",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Brake pads",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Brake fluid exchange",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Spark plug replacement",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Transmission fluid inspection",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Timing belt replacement",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Battery testing",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                },
                new Service
                {
                    Name = "Tire replacement",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in turpis tristique, feugiat sapien et, sollicitudin lacus. ",
                    IsDeleted = false
                }
            };
            await dbContext.Services.AddRangeAsync(services);
            await dbContext.SaveChangesAsync();
        }
    }
}
