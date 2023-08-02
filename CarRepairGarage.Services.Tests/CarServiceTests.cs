namespace CarRepairGarage.Services.Tests
{
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    
    using Moq;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Car;

    public class CarServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ILogger<CarService> _logger;
        private ICarService _carService;
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

            var loggerMock = new Mock<ILogger<CarService>>();
            _logger = loggerMock.Object;

            _mockRepository = new Mock<IRepository>();
            _carService = new CarService(_mockRepository.Object, _logger);
        }

        [Test]
        public async Task AddCarAsync_ShouldAddCarSuccessfully()
        {
            // Arrange
            var mockUser = new ApplicationUser { Id = Guid.NewGuid() };

            var carViewModel = new AddCarViewModel
            {
                VIN = "1HGCM82633A123456",
                Make = "Honda",
                CarModel = "Accord",
                Year = 2003
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Car>())).Callback<Car>((car) => _applicationDbContext.Add(car));
            _mockRepository.Setup(r => r.SaveChangesAsync()).Callback(() => _applicationDbContext.SaveChangesAsync());

            // Act
            await _carService.AddCarAsync(carViewModel, mockUser);

            // Assert
            var car = await _applicationDbContext.Cars.FirstOrDefaultAsync(c => c.VIN == carViewModel.VIN);
            Assert.NotNull(car);
            Assert.AreEqual(carViewModel.VIN, car.VIN);
            Assert.AreEqual(carViewModel.Make, car.Make);
            Assert.AreEqual(carViewModel.CarModel, car.Model);
            Assert.AreEqual(carViewModel.Year, car.Year);
            Assert.AreEqual(mockUser.Id, car.User.Id);
        }

        [Test]
        public async Task Delete_ShouldSetIsDeletedToTrue()
        {
            // Arrange
            var car = new Car { Id = 1, VIN = "1HGCM82633A123456", Make = "Honda", Model = "Accord", Year = 2003 };
            await _applicationDbContext.Cars.AddAsync(car);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.GetByIdAsync<Car>(It.IsAny<int>()))
                .Returns<int>(id => _applicationDbContext.Cars.FindAsync(id).AsTask());

            _mockRepository.Setup(r => r.SaveChangesAsync()).Callback(() => _applicationDbContext.SaveChangesAsync());

            // Act
            await _carService.Delete(car.Id);

            // Assert
            var deletedCar = await _applicationDbContext.Cars.FirstOrDefaultAsync(c => c.Id == car.Id);
            Assert.IsNotNull(deletedCar);
            if (deletedCar != null)
            {
                Assert.True(deletedCar.IsDeleted);
                Assert.NotNull(deletedCar.DeletedOn);
            }
        }

        [Test]
        public async Task Edit_ShouldUpdateCarDetails()
        {
            // Arrange
            var car = new Car
            {
                Id = 1,
                VIN = "OriginalVIN",
                Make = "OriginalMake",
                Model = "OriginalModel",
                Year = 2000,
                IsDeleted = false
            };

            await _applicationDbContext.Cars.AddAsync(car);
            await _applicationDbContext.SaveChangesAsync();

            var updatedCarViewModel = new AddCarViewModel
            {
                VIN = "UpdatedVIN",
                Make = "UpdatedMake",
                CarModel = "UpdatedModel",
                Year = 2001
            };

            _mockRepository.Setup(r => r.GetByIdAsync<Car>(It.IsAny<int>()))
                .Returns<int>(id => _applicationDbContext.Cars.FindAsync(id).AsTask());

            // Act
            await _carService.Edit(car.Id, updatedCarViewModel);

            // Assert
            var updatedCar = await _applicationDbContext.Cars.FirstOrDefaultAsync(c => c.Id == car.Id);
            Assert.IsNotNull(updatedCar);
            if (updatedCar != null)
            {
                Assert.AreEqual(updatedCar.VIN, updatedCarViewModel.VIN);
                Assert.AreEqual(updatedCar.Make, updatedCarViewModel.Make);
                Assert.AreEqual(updatedCar.Model, updatedCarViewModel.CarModel);
                Assert.AreEqual(updatedCar.Year, updatedCarViewModel.Year);
            }
        }

        [Test]
        public async Task Exist_ShouldReturnTrueIfCarExists()
        {
            // Arrange
            var car = new Car
            {
                Id = 1,
                VIN = "TestVIN",
                Make = "TestMake",
                Model = "TestModel",
                Year = 2000,
                IsDeleted = false
            };

            await _applicationDbContext.Cars.AddAsync(car);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Car>())
                .Returns(_applicationDbContext.Cars.AsQueryable());

            // Act
            var carExists = await _carService.Exist(car.Id);
            var nonExistentCarExists = await _carService.Exist(2);

            // Assert
            Assert.IsTrue(carExists);
            Assert.IsFalse(nonExistentCarExists);
        }

        [Test]
        public async Task GetAllCarsByUserIdAsync_ShouldReturnAllCarsByUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var user = new ApplicationUser { Id = userId };

            var cars = new List<Car>
            {
                new Car { Id = 1, VIN = "VIN1", Make = "Make1", Model = "Model1", Year = 2000, User = user, IsDeleted = false },
                new Car { Id = 2, VIN = "VIN2", Make = "Make2", Model = "Model2", Year = 2001, User = user, IsDeleted = false }
            };

            await _applicationDbContext.Cars.AddRangeAsync(cars);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Car>())
                .Returns(_applicationDbContext.Cars.AsQueryable());

            // Act
            var carsByUserId = await _carService.GetAllCarsByUserIdAsync(userId);

            // Assert
            Assert.AreEqual(2, carsByUserId.Count());
            Assert.IsTrue(carsByUserId.All(x => x.UserId == userId.ToString()));
        }

        [Test]
        public async Task GetCarByIdAsync_ShouldReturnCorrectCar()
        {
            // Arrange
            var expectedCar = new Car
            {
                Id = 1,
                VIN = "1234",
                Make = "Toyota",
                Model = "Camry",
                Year = 2010,
                User = new ApplicationUser() { Id = Guid.NewGuid() },
                IsDeleted = false
            };

            await _applicationDbContext.Cars.AddAsync(expectedCar);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.GetByIdAsync<Car>(It.IsAny<int>()))
                .Returns((int id) => _applicationDbContext.Cars.FirstOrDefaultAsync(c => c.Id == id));

            // Act
            var actualCarViewModel = await _carService.GetCarByIdAsync(expectedCar.Id);

            // Assert
            Assert.That(actualCarViewModel.Id, Is.EqualTo(expectedCar.Id));
            Assert.That(actualCarViewModel.VIN, Is.EqualTo(expectedCar.VIN));
            Assert.That(actualCarViewModel.Make, Is.EqualTo(expectedCar.Make));
            Assert.That(actualCarViewModel.CarModel, Is.EqualTo(expectedCar.Model));
            Assert.That(actualCarViewModel.Year, Is.EqualTo(expectedCar.Year));
            Assert.That(actualCarViewModel.UserId, Is.EqualTo(expectedCar.User.Id.ToString()));
        }

    }
}
