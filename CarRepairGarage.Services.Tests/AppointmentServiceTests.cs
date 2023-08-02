namespace CarRepairGarage.Services.Tests
{
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    
    using Moq;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Appointment;

    public class AppointmentServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ILogger<AppointmentService> _logger;
        private IAppointmentService _appointmentService;
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

            var loggerMock = new Mock<ILogger<AppointmentService>>();
            _logger = loggerMock.Object;

            _mockRepository = new Mock<IRepository>();
            _appointmentService = new AppointmentService(_mockRepository.Object, _logger);
        }

        [Test]
        public async Task TestCreateAppointmentAsync()
        {
            // Arrange
            var user = new ApplicationUser { /* fill properties if needed */ };
            var createAppointmentModel = new CreateAppointmentModel
            {
                ServiceId = 1,
                GarageId = 2,
                CarId = 3,
                SelectedDate = DateTime.Today,
                SelectedTime = DateTime.Now // or any other relevant DateTime
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Appointment>()))
                .Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            await _appointmentService.CreateAppointmentAsync(createAppointmentModel, user);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Appointment>()), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task TestDeleteAppointmentAsync()
        {
            // Arrange
            var appointmentId = Guid.NewGuid();

            _mockRepository.Setup(r => r.DeleteAsync<Appointment>(appointmentId.ToString()))
                .Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            await _appointmentService.Delete(appointmentId);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync<Appointment>(appointmentId.ToString()), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}

