namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;

    public class GarageService : IGarageService
    {
        private readonly IRepository _repository;
        public GarageService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GarageViewModel>> GetAllGaragesAsync(int count)
        {
            var garages = await _repository
                            .AllReadonly<Data.Models.Garage>()
                            .Where(x => x.IsDeleted == false)
                            .Include(x => x.Services)
                            .OrderByDescending(x => x.Id)
                            .Take(count)
                            .Select(x => new GarageViewModel()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Category = x.Category.Name,
                                City = x.Address.City.Name,
                                Services = x.Services.Select(x => x.Service.Name).ToList(),
                                ImageUrl = x.ImageUrl
                            }).ToListAsync();
            return garages;
        }

        public async Task<List<GarageServicesModel>> GetAllServicesByGarageIdAsync(int garageId)
        {
            var services = await _repository.AllReadonly<Data.Models.GarageService>()
                .Where(x => x.GarageId == garageId && x.IsDeleted == false)
                .Select(x => new GarageServicesModel()
                {
                    Id = x.Service.Id,
                    Name = x.Service.Name
                }).ToListAsync();

            return services;
        }
    }
}
