namespace CarRepairGarage.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    
    using Moq;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Note;

    [TestFixture]
    public class NoteServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private ILogger<NoteService> _logger;
        private INoteService _noteService;
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

            var loggerMock = new Mock<ILogger<NoteService>>();
            _logger = loggerMock.Object;

            _mockRepository = new Mock<IRepository>();
            _noteService = new NoteService(_mockRepository.Object, _logger);
        }

        [Test]
        public async Task TestCreateNote()
        {
            // Arrange
            var garages = new List<Garage>()
            {
                new Garage() { Id = 1, Note = null, AddressId = 1, Name = "Test Name", CategoryId = 1, ImageUrl = null, UserId = Guid.Parse("E1BDCB50-2474-47F7-6BCD-08DB908E902E"), IsDeleted = false },
            };

            // Add garages to the real ApplicationDbContext.
            await _applicationDbContext.Garages.AddRangeAsync(garages);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.All<Garage>())
                .Returns(_applicationDbContext.Garages.AsQueryable());

            _mockRepository.Setup(r => r.SaveChangesAsync())
                .Returns(() => _applicationDbContext.SaveChangesAsync());

            // Act
            await _noteService.CreateNoteAsync(new AddNoteViewModel
            {
                Description = "Description Test",
                GarageIds = new List<int> { 1 },
                Image = null,
                Title = "Title Test",
            });

            // Assert
            var dbNote = await _applicationDbContext.Notes.FirstAsync();

            Assert.IsNotNull(dbNote);
            Assert.That(dbNote.Description, Is.EqualTo("Description Test"));
            Assert.That(dbNote.Title, Is.EqualTo("Title Test"));
        }

        [Test]
        public async Task TestDeleteNote()
        {

            var garage = new Garage { 
                Id = 1, 
                Note = new Note { Id = 1, Title = "Test Note", Description = "Some Test Description", IsDeleted = false },
                AddressId = 1, 
                Name = "Test Name", 
                CategoryId = 1, 
                ImageUrl = null, 
                UserId = Guid.Parse("E1BDCB50-2474-47F7-6BCD-08DB908E902E"), 
                IsDeleted = false };

            _applicationDbContext.Garages.Add(garage);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.All<Garage>())
                .Returns(_applicationDbContext.Garages.AsQueryable());

            _mockRepository.Setup(r => r.SaveChangesAsync())
                .Returns(() => _applicationDbContext.SaveChangesAsync());

            await _noteService.Delete(1);

            var dbGarage = await _applicationDbContext.Garages.FirstAsync();

            Assert.IsNull(dbGarage.Note);
            Assert.IsNull(dbGarage.NoteId);

            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task TestDeleteAllNotes()
        {
            var note = new Note { Id = 1, Title = "Test Note", Description = "Some Test Description", IsDeleted = false };
            var garages = new List<Garage>
            {
                new Garage { Id = 1, Note = note, AddressId = 1,Name = "Test Garage 1",CategoryId = 1,ImageUrl = null,UserId = Guid.NewGuid(),IsDeleted = false  },
                new Garage { Id = 2, Note = note,AddressId = 1,Name = "Test Garage 2",CategoryId = 1,ImageUrl = null,UserId = Guid.NewGuid(),IsDeleted = false   },
                new Garage { Id = 3, Note = note,AddressId = 1,Name = "Test Garage 3",CategoryId = 1,ImageUrl = null,UserId = Guid.NewGuid(),IsDeleted = false   },
            };
            note.Garages = garages;

            _applicationDbContext.Notes.Add(note);
            _applicationDbContext.Garages.AddRange(garages);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.All<Note>())
                .Returns(_applicationDbContext.Notes
                    .Include(x => x.Garages)
                    .AsQueryable());

            _mockRepository.Setup(r => r.SaveChangesAsync())
                .Returns(() => _applicationDbContext.SaveChangesAsync());

            await _noteService.DeleteAll(1);

            var dbGarages = await _applicationDbContext.Garages.ToListAsync();

            Assert.IsTrue(dbGarages.All(garage => garage.NoteId == null));

            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task TestExistNote()
        {
            var notes = new List<Note>
            {
                new Note { Id = 1, Title = "Test Note 1", Description = "Some Test Description", IsDeleted = false  },
                new Note { Id = 2, Title = "Test Note 2", Description = "Some Test Description", IsDeleted = false  },
                new Note { Id = 3, Title = "Test Note 3", Description = "Some Test Description", IsDeleted = false  },
            };

            // Add notes to the real ApplicationDbContext.
            await _applicationDbContext.Notes.AddRangeAsync(notes);
            await _applicationDbContext.SaveChangesAsync();

            _mockRepository.Setup(r => r.AllReadonly<Note>())
                .Returns(_applicationDbContext.Notes.AsQueryable());

            var exists = await _noteService.Exist(2);

            Assert.IsTrue(exists);

            exists = await _noteService.Exist(5);

            Assert.IsFalse(exists);
        }

    }
}