namespace CarRepairGarage.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    
    using Moq;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;

    public class CategoryServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ICategoryService _categoryService;
        private ApplicationDbContext _applicationDbContext;
        private ILogger<CategoryService> _logger;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _applicationDbContext = new ApplicationDbContext(contextOptions);

            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();

            var loggerMock = new Mock<ILogger<CategoryService>>();
            _logger = loggerMock.Object;

            _mockRepository = new Mock<IRepository>();
            _categoryService = new CategoryService(_mockRepository.Object, _logger);
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
