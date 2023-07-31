namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Note;
    using CarRepairGarage.Web.ViewModels.Garage;

    public class NoteService : BaseService, INoteService
    {
        private readonly IRepository _repository;
        private readonly ILogger<NoteService> _logger;

        public NoteService(
            IRepository repository,
            ILogger<NoteService> logger) : base(logger)
        {
            _repository = repository;
        }
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

        public async Task<bool> Exist(int id)
        {
            return await _repository.AllReadonly<Note>()
                .AnyAsync(x => x.Id == id);
        }

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
