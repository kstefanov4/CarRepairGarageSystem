using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Data;
using CarRepairGarage.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using CarRepairGarage.Data.Models;

namespace CarRepairGarage.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    
    using Moq;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using Microsoft.Extensions.Logging;

    [TestFixture]
    public class ServiseServiseTests
    {
        private Mock<IRepository> _mockRepository;
        private IServiceService _serviceService;
        private ILogger<ServiceService> _logger;
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

            var loggerMock = new Mock<ILogger<ServiceService>>();
            _logger = loggerMock.Object;

            _mockRepository = new Mock<IRepository>();
            _serviceService = new ServiceService(_mockRepository.Object, _logger);
        }

        [Test]
        public async Task TestAllServicesNameAsync()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service { Id = 1, Name = "Test Service 1", Description = "Some Test Description", IsDeleted = false },
                new Service { Id = 2, Name = "Test Service 2", Description = "Some Test Description", IsDeleted = false },
                new Service { Id = 3, Name = "Test Service 3", Description = "Some Test Description", IsDeleted = false },
            };

            // Add services to the real ApplicationDbContext.
            await _applicationDbContext.Services.AddRangeAsync(services);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Service>())
                .Returns(_applicationDbContext.Services.AsQueryable());

            // Act
            var serviceNames = await _serviceService.AllServicesNameAsync();

            // Assert
            Assert.That(serviceNames.Count(), Is.EqualTo(3));
            Assert.That(serviceNames, Contains.Item("Test Service 1"));
            Assert.That(serviceNames, Contains.Item("Test Service 2"));
            Assert.That(serviceNames, Contains.Item("Test Service 3"));
        }

        [Test]
        public async Task TestGetAllServiceAsync()
        {
            // Arrange
            
            var services = new List<Service>();
            var garages = new List<Garage>
            {
                new Garage { Id = 1, Note = null, AddressId = 1,Name = "Test Garage 1",CategoryId = 1,ImageUrl = null,UserId = Guid.NewGuid(),IsDeleted = false},
                new Garage { Id = 2, Note = null, AddressId = 1,Name = "Test Garage 2",CategoryId = 1,ImageUrl = null,UserId = Guid.NewGuid(),IsDeleted = false},
                new Garage { Id = 3, Note = null, AddressId = 1,Name = "Test Garage 2",CategoryId = 1,ImageUrl = null,UserId = Guid.NewGuid(),IsDeleted = false}
            };

            // create services with unique garage services
            for (int i = 0; i < 3; i++)
            {
                var garageServices = new List<Data.Models.GarageService>
                {
                    new Data.Models.GarageService { GarageId = garages[i].Id, Garage = garages[i], ServiceId = i + 1 }
                };

                services.Add(new Service
                {
                    Id = i + 1,
                    Name = $"Test Service {i + 1}",
                    Description = "Some Test Description",
                    IsDeleted = false,
                    Garages = garageServices
                });
            }

            // Add services to the real ApplicationDbContext.
            await _applicationDbContext.Services.AddRangeAsync(services);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Service>())
                .Returns(_applicationDbContext.Services
                    .Include(x => x.Garages)
                    .ThenInclude(x => x.Garage)
                    .AsQueryable());


            // Act
            var serviceViewModels = await _serviceService.GetAllServiceAsync();

            // Assert
            Assert.That(serviceViewModels.Count, Is.EqualTo(3));

            foreach (var serviceViewModel in serviceViewModels)
            {
                Assert.That(serviceViewModel.Id, Is.GreaterThan(0));
                Assert.That(serviceViewModel.Name, Does.StartWith("Test Service"));
                Assert.That(serviceViewModel.Description, Is.EqualTo("Some Test Description"));

                foreach (var garageViewModel in serviceViewModel.Garages)
                {
                    Assert.That(garageViewModel.Id, Is.GreaterThan(0));
                    Assert.That(garageViewModel.Name, Does.StartWith("Test Garage"));
                }
            }
        }

        [Test]
        public async Task TestGetServiceByIdAsync()
        {
            // Arrange
            var services = new List<Service>
            {
                new Service { Id = 1, Name = "Test Service 1", Description = "Some Test Description", IsDeleted = false },
                new Service { Id = 2, Name = "Test Service 2", Description = "Some Test Description", IsDeleted = true },
                new Service { Id = 3, Name = "Test Service 3", Description = "Some Test Description", IsDeleted = false },
            };

            // Add services to the real ApplicationDbContext.
            await _applicationDbContext.Services.AddRangeAsync(services);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Service>())
                .Returns(_applicationDbContext.Services.AsQueryable());

            // Act
            var serviceName = await _serviceService.GetServiceByIdAsync(1);

            // Assert
            Assert.That(serviceName, Is.EqualTo("Test Service 1"));
        }

    }
}
