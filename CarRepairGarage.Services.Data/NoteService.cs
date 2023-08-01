namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Note;

    /// <summary>
    /// Service class for managing notes-related operations.
    /// </summary>
    public class NoteService : BaseService, INoteService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        /// <param name="logger">The logger for logging.</param>
        public NoteService(
            IRepository repository,
            ILogger<NoteService> logger) : base(logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new note with the provided information and associates it with one or more garages.
        /// </summary>
        /// <param name="model">The <see cref="AddNoteViewModel"/> containing the note information.</param>
        public async Task CreateNoteAsync(AddNoteViewModel model)
        {
            string imageUrl = await GetImagePath(model);

            Note note = new Note()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = imageUrl,
                    Vissible = true
                };

            foreach (var garageId in model.GarageIds)
            {
                var garage = await _repository.All<Garage>()
                    .Include(x => x.Note)
                    .Where(x => x.Id == garageId)
                    .FirstAsync();

                garage.Note = note;

            }

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Deletes the note associated with the specified garage ID.
        /// </summary>
        /// <param name="id">The ID of the garage whose note to delete.</param>
        public async Task Delete(int id)
        {
            var garage = await _repository.All<Garage>()
                .Where(x => x.Id == id)
                .FirstAsync();
            garage.Note = null;
            garage.NoteId = null;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Deletes the association of the note with all the garages.
        /// </summary>
        /// <param name="id">The ID of the note whose associations to delete.</param>
        public async Task DeleteAll(int id)
        {
            var note = await _repository.All<Note>()
            .Include(x => x.Garages)
            .Where(x => x.Id == id)
            .FirstAsync();

            foreach (var garage in note.Garages)
            {
                garage.NoteId = null;
            }

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Checks if a note with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the note to check.</param>
        /// <returns><c>true</c> if the note exists; otherwise, <c>false</c>.</returns>
        public async Task<bool> Exist(int id)
        {
            return await _repository.AllReadonly<Note>()
                .AnyAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets the image path for the note from the provided <see cref="AddNoteViewModel"/>.
        /// </summary>
        /// <param name="model">The <see cref="AddNoteViewModel"/> containing the image file.</param>
        /// <returns>The URL of the uploaded image.</returns>
        private static async Task<string> GetImagePath(AddNoteViewModel model)
        {
            string imageUrl = null;
            if (model.Image != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                imageUrl = "/images/" + fileName;
            }

            return imageUrl;
        }
    }
}
