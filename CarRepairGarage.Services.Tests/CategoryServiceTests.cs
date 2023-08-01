using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Data;
using CarRepairGarage.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRepairGarage.Data.Models;

namespace CarRepairGarage.Services.Tests
{
    public class CategoryServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ICategoryService _categoryService;
        private ApplicationDbContext _applicationDbContext;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _applicationDbContext = new ApplicationDbContext(contextOptions);

            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();

            _mockRepository = new Mock<IRepository>();
            _categoryService = new CategoryService(_mockRepository.Object);
        }

        [Test]
        public async Task AllCategoriesNameAsync_ShouldReturnAllCategoryNames()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Name = "Category1", Description = "Teast Description", ImageUrl = "https", IsDeleted = false },
                new Category { Name = "Category2", Description = "Teast Description", ImageUrl = "https", IsDeleted = false },
                new Category { Name = "Category3", Description = "Teast Description", ImageUrl = "https", IsDeleted = false },
            };

            await _applicationDbContext.Categories.AddRangeAsync(categories);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Category>())
                .Returns(_applicationDbContext.Categories);

            // Act
            var categoryNames = await _categoryService.AllCategoriesNameAsync();

            // Assert
            Assert.That(categoryNames.Count(), Is.EqualTo(3));
            Assert.That(categoryNames.Contains("Category1"));
            Assert.That(categoryNames.Contains("Category2"));
            Assert.That(categoryNames.Contains("Category3"));
        }

    }
}
