namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Category;
    using CarRepairGarage.Web.ViewModels.Garage;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoryAsync(int count)
        {
            var model = await this.repository.AllReadonly<Category>()
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Garages)
                .Select(x => new CategoryViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Garages = x.Garages.Select(g => new GarageViewModel
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList()
                }).ToListAsync();

            return model;
        }
    }
}
