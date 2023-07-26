namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using System.Linq;
    using CarRepairGarage.Web.ViewModels.Garage.Enums;
    using Microsoft.Extensions.Logging;
    using CarRepairGarage.Web.ViewModels.Service;

    public class GarageService : IGarageService
    {
        private readonly IRepository _repository;
        private readonly ILogger<GarageService> _logger;
        public GarageService(
            IRepository repository,
            ILogger<GarageService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task AddGarageAsync(AddGarageViewModel model, ApplicationUser user)
        {
            City city = new City()
            {
                Name = model.City
            };

            Address address = new Address()
            {
                City = city,
                StreetName = model.StreetName,
                StreetNumber = model.StreetNumber
            };

            Garage garage = new Garage()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Address = address,
                Owner = user,
                
            };

            List<Data.Models.GarageService> services = new List<Data.Models.GarageService>();
            
            foreach (var serviceId in model.ServiceIds)
            {
                Data.Models.GarageService garageService = new Data.Models.GarageService()
                {
                    Garage = garage,
                    ServiceId = serviceId,
                    Available = true
                };
                services.Add(garageService);
            }


            try
            {
                await _repository.AddAsync(city);
                await _repository.AddAsync(address);
                await _repository.AddAsync(garage);

                foreach (var service in services)
                {
                    await _repository.AddAsync(service);
                }

                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(AddGarageAsync), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }

        public async Task<AllGaragesFilteredAndPagedServiceModel> AllAsync(AllGaragesQueryModel queryModel)
        {
            IQueryable<Garage> garagesQuery = _repository.All<Garage>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                garagesQuery = garagesQuery.Where(x => x.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Service))
            {
                garagesQuery = garagesQuery.Where(x => x.Services.Any(g => g.Service.Name == queryModel.Service));
            }

            if (!string.IsNullOrWhiteSpace(queryModel.City))
            {
                garagesQuery = garagesQuery.Where(x => x.Address.City.Name == queryModel.City);
            }

            if (!string.IsNullOrEmpty(queryModel.SearchByString))
            {
                string wildCard = $"%{queryModel.SearchByString.ToLower()}%";

                garagesQuery = garagesQuery.Where(x =>
                    EF.Functions.Like(x.Category.Name, wildCard) ||
                    EF.Functions.Like(x.Name, wildCard) ||
                    EF.Functions.Like(x.Address.StreetName, wildCard) ||
                    EF.Functions.Like(x.Address.City.Name, wildCard) ||
                    x.Services.Any(s => EF.Functions.Like(s.Service.Name, wildCard))
                );
            }

            // Sorting
            switch (queryModel.GarageSorting)
            {
                case GarageSorting.Newest:
                    garagesQuery = garagesQuery.OrderByDescending(x => x.Id);
                    break;
                case GarageSorting.Oldest:
                    garagesQuery = garagesQuery.OrderBy(x => x.Id);
                    break;
                default:
                    garagesQuery = garagesQuery.OrderByDescending(x => x.Name);
                    break;
            }

            // Total count of filtered garages
            int totalGarages = garagesQuery.Count();

            // Pagination
            garagesQuery = garagesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.GaragesPerPage)
                .Take(queryModel.GaragesPerPage);

            IEnumerable<GarageViewModel> allGarages = await garagesQuery
                .Select(x => new GarageViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Services = x.Services.Select(s => s.Service.Name).ToList(),
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    City = x.Address.City.Name
                })
                .ToArrayAsync();

            return new AllGaragesFilteredAndPagedServiceModel()
            {
                TotalGarageCount = totalGarages,
                Garages = allGarages
            };
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.AllReadonly<Garage>()
                .AnyAsync(x => x.Id == id && x.IsDeleted == false);
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

        public async Task<IEnumerable<GarageViewModel>> GetAllGaragesByOwnerAsync(string id)
        {
            var garages = await _repository.AllReadonly<Garage>()
                .Where(x => x.UserId.ToString() == id && x.IsDeleted == false)
                .Select(x => new GarageViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    City = x.Address.City.Name,
                    Services = x.Services.Select(x => x.Service.Name).ToList(),
                    ImageUrl = x.ImageUrl,
                    StreetName = x.Address.StreetName,
                    StreetNumber = x.Address.StreetNumber.ToString()
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

        public async Task<GarageViewModel> GetGarageByIdAsync(int id)
        {
            GarageViewModel? garage = await _repository.AllReadonly<Garage>()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(g => new GarageViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    OwnerId = g.Owner.Id,
                    ImageUrl = g.ImageUrl,
                    City = g.Address.City.Name,
                    StreetName = g.Address.StreetName,
                    StreetNumber = g.Address.StreetNumber.ToString(),
                    Services = g.Services.Select(s => s.Service.Name).ToList(),
                    Category = g.Category.Name

                })
                .FirstOrDefaultAsync();

            return garage!;
        }

        public async Task<ModifyGarageViewModel> ModifyGarageByIdAsync(int id)
        {
            ModifyGarageViewModel? garage = await _repository.AllReadonly<Garage>()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(x => new ModifyGarageViewModel()
                {
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    City = x.Address.City.Name,
                    ImageUrl = x.ImageUrl,
                    StreetName = x.Address.StreetName,
                    StreetNumber = x.Address.StreetNumber,
                    Services = x.Services.Select(s => s.Service.Id).ToList()
                }).FirstOrDefaultAsync();

            return garage;
        }
    }
}
