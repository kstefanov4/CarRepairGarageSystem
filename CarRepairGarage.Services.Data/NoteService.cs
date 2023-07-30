using CarRepairGarage.Data.Models;
using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Note;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository _repository;
        private readonly ILogger<NoteService> _logger;

        public NoteService(
            IRepository repository,
            ILogger<NoteService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task CreateNoteAsync(AddNoteViewModel model)
        {
            Note note = new Note()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
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

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(CreateNoteAsync), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
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

        private async Task ExecuteDatabaseAction(Func<Task> databaseAction)
        {
            try
            {
                await databaseAction.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogError(databaseAction.Method.Name, ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }
    }
}
