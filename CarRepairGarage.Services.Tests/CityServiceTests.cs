namespace CarRepairGarage.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    
    using Moq;
    
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data;
    using CarRepairGarage.Services.Contracts;


    [TestFixture]
    public class CityServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ICityService _cityService;
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
            _cityService = new CityService(_mockRepository.Object);
        }

        [Test]
        public async Task AllCitiesNameAsync_ShouldReturnAllUndeletedCityNames()
        {
            // Arrange
            var cities = new List<City>
            {
                new City { Id = 1, Name = "City1", IsDeleted = false },
                new City { Id = 2, Name = "City2", IsDeleted = false },
                new City { Id = 3, Name = "City3", IsDeleted = true } // This city is deleted
            };

            // Add cities to the real ApplicationDbContext.
            await _applicationDbContext.Cities.AddRangeAsync(cities);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<City>())
                .Returns(_applicationDbContext.Cities.AsQueryable());

            // Act
            var result = await _cityService.AllCitiesNameAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
            CollectionAssert.Contains(result, "City1");
            CollectionAssert.Contains(result, "City2");
            CollectionAssert.DoesNotContain(result, "City3");
        }

        [Test]
        public async Task Exist_ShouldReturnTrue_WhenCityWithNameExists()
        {
            // Arrange
            var cities = new List<City>
            {
                new City { Id = 1, Name = "City1", IsDeleted = false },
                new City { Id = 2, Name = "City2", IsDeleted = false },
                new City { Id = 3, Name = "City3", IsDeleted = true } // This city is deleted
            };

            // Add cities to the real ApplicationDbContext.
            await _applicationDbContext.Cities.AddRangeAsync(cities);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<City>())
                .Returns(_applicationDbContext.Cities.AsQueryable());

            // Act
            var existCity1 = await _cityService.Exist("City1");
            var existCity3 = await _cityService.Exist("City3");
            var existCity4 = await _cityService.Exist("City4");

            // Assert
            Assert.IsTrue(existCity1);
            Assert.IsTrue(existCity3);
            Assert.IsFalse(existCity4);
        }

        [Test]
        public async Task GetCityByNameAsync_ShouldReturnCorrectCity_WhenCityWithNameExists()
        {
            // Arrange
            var cities = new List<City>
            {
                new City { Id = 1, Name = "City1", IsDeleted = false },
                new City { Id = 2, Name = "City2", IsDeleted = false },
                new City { Id = 3, Name = "City3", IsDeleted = true } // This city is deleted
            };

            // Add cities to the real ApplicationDbContext.
            await _applicationDbContext.Cities.AddRangeAsync(cities);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<City>())
                .Returns(_applicationDbContext.Cities.AsQueryable());

            // Act
            var city1 = await _cityService.GetCityByNameAsync("City1");
            var city3 = await _cityService.GetCityByNameAsync("City3");

            // Assert
            Assert.IsNotNull(city1);
            Assert.AreEqual("City1", city1.Name);
            Assert.IsNotNull(city3);
            Assert.AreEqual("City3", city3.Name);
        }

        [Test]
        public void GetCityByNameAsync_ShouldThrowException_WhenCityWithNameDoesNotExist()
        {
            // Arrange
            var cities = new List<City>
            {
                new City { Id = 1, Name = "City1", IsDeleted = false },
                new City { Id = 2, Name = "City2", IsDeleted = false }
            };

            // Add cities to the real ApplicationDbContext.
            _applicationDbContext.Cities.AddRange(cities);
            _applicationDbContext.SaveChanges();

            _mockRepository.Setup(r => r.AllReadonly<City>())
                .Returns(_applicationDbContext.Cities.AsQueryable());

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _cityService.GetCityByNameAsync("City4"));
        }

    }
}
