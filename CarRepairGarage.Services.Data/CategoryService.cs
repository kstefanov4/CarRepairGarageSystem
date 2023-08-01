﻿namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Category;
    using CarRepairGarage.Web.ViewModels.Garage;

    /// <summary>
    /// Service class for managing category-related operations.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        public CategoryService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all category names from the database.
        /// </summary>
        /// <returns>A collection of category names as strings.</returns>
        public async Task<IEnumerable<string>> AllCategoriesNameAsync()
        {
            return await _repository.AllReadonly<Category>()
                .Select(c => c.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all categories from the database along with their associated garages.
        /// </summary>
        /// <returns>A collection of <see cref="CategoryViewModel"/> representing the categories.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoryAsync()
        {
            var model = await _repository.AllReadonly<Category>()
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

        /// <summary>
        /// Retrieves the name of the category with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The name of the category as a string.</returns>
        public async Task<string> GetCategoryByIdAsync(int id)
        {
            return await _repository.AllReadonly<Category>()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(x => x.Name)
                .FirstAsync();
        }
    }

}
