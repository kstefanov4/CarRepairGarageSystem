namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    
    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            Category[] categories = new Category[]
            {
                new Category
                {
                    Name = "Interim Car Service",
                    Description = "An interim service is typically smaller-scale maintenance that occurs between full services,  generally every six months or 6,000 miles.",
                    ImageUrl = "https://images.prismic.io/leasefetcher/d53c80bd-d399-4351-aca0-910cab702942_car-mechanic-cleaning-inside-car.jpg?auto=compress,format&rect=0,0,1200,680&w=1200&h=680",
                    IsDeleted = false
                },
                new Category
                {
                    Name = "Full Car Service",
                    Description = "It's generally said that every driver should bring their car in for a full service once a year or every 12,000 miles.A full service typically includes all the checks of an interim service, plus inspecting several other key components.",
                    ImageUrl = "https://images.prismic.io/leasefetcher/be5e21ee-22da-492c-85e2-34c3e7f70bec_mechanic-doing-checks-car-service.jpg?auto=compress,format&rect=0,0,1200,680&w=1200&h=680",
                    IsDeleted = false
                },
                new Category
                {
                    Name = "Major Car Service",
                    Description = "Major car services are often reserved for the important maintenance items that don’t require servicing as often as other parts. Major car services may include all the checks from an interim and full service, plus other critical items.",
                    ImageUrl = "https://www.autoleaders.com.au/wp-content/uploads/2019/10/AdobeStock_205382466.jpg",
                    IsDeleted = false
                }
            };
            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
