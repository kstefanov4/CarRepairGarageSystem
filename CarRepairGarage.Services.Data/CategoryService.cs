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
        private readonly IRepository _repository;

        public CategoryService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<string>> AllCategoriesNameAsync()
        {
            return await _repository.AllReadonly<Category>()
                .Select(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoryAsync()
        {
            var model = await this._repository.AllReadonly<Category>()
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
