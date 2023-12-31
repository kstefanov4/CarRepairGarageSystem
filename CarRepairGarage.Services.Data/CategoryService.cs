﻿namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Category;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Services.Helpers;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service class for managing category-related operations.
    /// </summary>
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        /// <param name="logger">The logger for logging service events.</param>
        public CategoryService(
            IRepository repository,
            ILogger<CategoryService> logger) : base(logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Adds a new category to the database based on the provided <paramref name="model"/>.
        /// </summary>
        /// <param name="model">The <see cref="AddCategoryViewModel"/> containing the data for the new category.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(AddCategoryViewModel model)
        {
            var image = await ImageUtility.GetImagePath(model.ImageUrl);

            Category category = new Category()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = image,
                IsDeleted = false
            };

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.AddAsync(category);
                await _repository.SaveChangesAsync();
            });
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
        /// Deletes a category from the database based on the provided <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var car = await _repository.GetByIdAsync<Category>(id);
            car.IsDeleted = true;
            car.DeletedOn = DateTime.Now;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Checks if a category with the specified <paramref name="id"/> exists in the database.
        /// </summary>
        /// <param name="id">The ID of the category to check.</param>
        /// <returns>True if the category exists; otherwise, false.</returns>
        public async Task<bool> Exist(int id)
        {
            return await _repository.AllReadonly<Category>()
                .AnyAsync(x => x.Id == id);
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
        /// Retrieves the name of the category with the specified <paramref name="id"/> from the database.
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
