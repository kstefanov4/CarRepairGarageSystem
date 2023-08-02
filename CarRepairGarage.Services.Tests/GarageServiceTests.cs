using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Data;
using CarRepairGarage.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using CarRepairGarage.Data.Models;
using CarRepairGarage.Web.ViewModels.Garage;

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

        [Test]
        public async Task Exists_ShouldReturnTrue_WhenGarageExistsAndIsNotDeleted()
        {
            // Arrange
            var garage = new Garage { Id = 1, Note = null, AddressId = 1, Name = "Test Name", CategoryId = 1, ImageUrl = null, UserId = Guid.NewGuid(), IsDeleted = false };
            _applicationDbContext.Garages.Add(garage);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Garage>())
                .Returns(_applicationDbContext.Garages.AsQueryable());

            // Act
            var exists = await _garageService.Exists(garage.Id);

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task GetAllGaragesAsync_ShouldReturnCorrectGarages_WhenCalledWithValidCount()
        {
            // Arrange
            var garages = new List<Garage>
            {
                new Garage { 
                    Id = 1, 
                    IsDeleted = false, 
                    Name = "Garage1", 
                    Category = new Category { 
                        Name = "Category1", 
                        ImageUrl = "Url1", 
                        Description = "TestDescription" }, 
                    Address = new Address { 
                        City = new City { 
                            Name = "City1" }, 
                        StreetName = "StreetName", 
                        StreetNumber = 2 }, 
                    Services = new List<Data.Models.GarageService> { 
                        new Data.Models.GarageService { 
                            Service = new Service { 
                                Name = "Service1" } } }, 
                    ImageUrl = "Url1" },

                new Garage { 
                    Id = 2, 
                    IsDeleted = true, 
                    Name = "Garage2", 
                    Category = new Category { 
                        Name = "Category2", 
                        ImageUrl = "Url1", 
                        Description = "TestDescription" }, 
                    Address = new Address { 
                        City = new City { 
                            Name = "City2" }, 
                        StreetName = "StreetName", 
                        StreetNumber = 2 }, 
                    Services = new List<Data.Models.GarageService> { 
                        new Data.Models.GarageService { 
                            Service = new Service { 
                                Name = "Service2" } } }, 
                    ImageUrl = "Url2" },
                new Garage { 
                    Id = 3, 
                    IsDeleted = false, 
                    Name = "Garage3", 
                    Category = new Category { 
                        Name = "Category3", 
                        ImageUrl = "Url1", 
                        Description = "TestDescription" }, 
                    Address = new Address { 
                        City = new City { 
                            Name = "City3" }, 
                        StreetName = "StreetName", 
                        StreetNumber = 2 }, 
                    Services = new List<Data.Models.GarageService> { 
                        new Data.Models.GarageService { 
                            Service = new Service { 
                                Name = "Service3" } } }, 
                    ImageUrl = "Url3" }
            };

            foreach (var garage in garages)
            {
                _applicationDbContext.Garages.Add(garage);
            }

            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Garage>())
                .Returns(_applicationDbContext.Garages.Include(x => x.Services).AsQueryable());

            var expectedCount = 2; // Only two garages are not marked as deleted

            // Act
            var resultGarages = await _garageService.GetAllGaragesAsync(expectedCount);

            // Assert
            Assert.IsNotNull(resultGarages);
            Assert.AreEqual(expectedCount, resultGarages.Count());
            Assert.AreEqual("Garage3", resultGarages.First().Name); // Expect the garage with the highest ID
            Assert.AreEqual("Category3", resultGarages.First().Category);
            Assert.AreEqual("City3", resultGarages.First().City);
            Assert.AreEqual("Service3", resultGarages.First().Services.First());
            Assert.AreEqual("Url3", resultGarages.First().ImageUrl);

        }

        [Test]
        public async Task Delete_ShouldMarkGarageAsDeleted_WhenCalledWithValidId()
        {
            // Arrange
            var garageId = 1;
            var garage = new Garage
            {
                Id = 1,
                IsDeleted = false,
                Name = "Garage1",
                Category = new Category
                {
                    Name = "Category1",
                    ImageUrl = "Url1",
                    Description = "TestDescription"
                },
                Address = new Address
                {
                    City = new City
                    {
                        Name = "City1"
                    },
                    StreetName = "StreetName",
                    StreetNumber = 2
                },
                Services = new List<Data.Models.GarageService> {
                        new Data.Models.GarageService {
                            Service = new Service {
                                Name = "Service1" } } },
                ImageUrl = "Url1"
            };

            _mockRepository.Setup(r => r.GetByIdAsync<Garage>(garageId))
            .ReturnsAsync(garage);
            _mockRepository.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1) // assuming SaveChangesAsync returns the number of affected rows
                .Verifiable();

            // Act
            await _garageService.Delete(garageId);

            // Assert
            Assert.IsTrue(garage.IsDeleted);
            Assert.IsNotNull(garage.DeletedOn);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }



    }
}

