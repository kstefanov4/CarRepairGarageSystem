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
using CarRepairGarage.Web.ViewModels.Garage;
using CarRepairGarage.Web.ViewModels.Garage.Enums;

namespace CarRepairGarage.Services.Tests
{
    public class GarageServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ILogger<GarageService> _logger;
        private IGarageService _garageService;
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

            var loggerMock = new Mock<ILogger<GarageService>>();
            _logger = loggerMock.Object;

            _mockRepository = new Mock<IRepository>();
            _garageService = new GarageService(_mockRepository.Object, _logger);

        }

        [Test]
        public async Task AddGarageAsync_ShouldAddGarage_WhenDataIsValid()
        {
            // Arrange
            var testCity = new City { Id = 1, Name = "TestCity" };
            var testUser = new ApplicationUser { Id = Guid.NewGuid(), UserName = "TestUser" };
            var testAddGarageViewModel = new AddGarageViewModel
            {
                Name = "Test Garage",
                CategoryId = 1,
                Image = null,
                StreetName = "Test Street",
                StreetNumber = 1,
                City = "TestCity",
                ServiceIds = new List<int> { 1, 2, 3 }
            };

            _applicationDbContext.Cities.Add(testCity);
            _applicationDbContext.Users.Add(testUser);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<City>())
                .Returns(_applicationDbContext.Cities.AsQueryable());

            _mockRepository.Setup(r => r.All<City>())
                .Returns(_applicationDbContext.Cities.AsQueryable());

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Garage>()))
                .Callback((Garage garage) => _applicationDbContext.Garages.Add(garage));

            _mockRepository.Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<Data.Models.GarageService>>()))
                .Callback((IEnumerable<Data.Models.GarageService> garageServices) => _applicationDbContext.GaragesServices.AddRange(garageServices));


            _mockRepository.Setup(r => r.SaveChangesAsync())
                .Returns(() => _applicationDbContext.SaveChangesAsync());

            // Act
            await _garageService.AddGarageAsync(testAddGarageViewModel, testUser);

            // Assert
            var dbGarage = await _applicationDbContext.Garages.FirstAsync();
            var dbGarageServices = await _applicationDbContext.GaragesServices.ToListAsync();

            Assert.IsNotNull(dbGarage);
            Assert.That(dbGarage.Name, Is.EqualTo("Test Garage"));
            Assert.That(dbGarage.Address.City.Name, Is.EqualTo("TestCity"));
            Assert.That(dbGarageServices, Is.Not.Empty);
            Assert.That(dbGarageServices.Count, Is.EqualTo(testAddGarageViewModel.ServiceIds.Count));
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

    }
}

