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
    using System.Net;
    using System.Runtime.Loader;
    using CarRepairGarage.Web.ViewModels.Note;

    public class GarageService : IGarageService
    {
        private readonly IRepository _repository;
        private readonly ILogger<GarageService> _logger;
        private readonly ICityService _cityService;
        public GarageService(
            IRepository repository,
            ILogger<GarageService> logger,
            ICityService cityService)
        {
            _repository = repository;
            _logger = logger;
            _cityService = cityService;

        }

        public async Task AddGarageAsync(AddGarageViewModel model, ApplicationUser user)
        {
            var city = CreateCity(model.City);
            var address = CreateAddress(city, model.StreetName, model.StreetNumber);
            var garage = CreateGarage(model, user, address);

            var services = model.ServiceIds
                .Select(serviceId => CreateGarageService(garage, serviceId))
                .ToList();

            try
            {
                await _repository.AddAsync(city);
                await _repository.AddAsync(address);
                await _repository.AddAsync(garage);
                await _repository.AddRangeAsync(services); // batch add

                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(AddGarageAsync), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }

        private City CreateCity(string cityName)
        {
            return new City { Name = cityName };
        }

        private Address CreateAddress(City city, string streetName, int streetNumber)
        {
            return new Address
            {
                City = city,
                StreetName = streetName,
                StreetNumber = streetNumber
            };
        }

        private Garage CreateGarage(AddGarageViewModel model, ApplicationUser user, Address address)
        {
            return new Garage
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Address = address,
                Owner = user
            };
        }

        private Data.Models.GarageService CreateGarageService(Garage garage, int serviceId)
        {
            return new Data.Models.GarageService
            {
                Garage = garage,
                ServiceId = serviceId,
                Available = true
            };
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

        public async Task Edit(ModifyGarageViewModel model)
        {
            var garage = await _repository.GetByIdAsync<Garage>(model.Id);
            var services = await GetGarageServices(garage.Id);

            await HandleServices(model, garage, services);

            await HandleCityAndAddress(model, garage);

            garage.Name = model.Name;
            garage.ImageUrl = model.ImageUrl;
            garage.CategoryId = model.CategoryId;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(Edit), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }

        private async Task<List<Data.Models.GarageService>> GetGarageServices(int garageId)
        {
            return await _repository.AllReadonly<Data.Models.GarageService>()
                .Where(x => x.GarageId == garageId)
                .ToListAsync();
        }

        private async Task HandleServices(ModifyGarageViewModel model, Garage garage, List<Data.Models.GarageService> services)
        {
            var serviceIdsToDelete = services.Where(service => !model.Services.Contains(service.ServiceId))
                .Select(service => service.ServiceId)
                .ToList();

            var addedServices = model.Services.Where(serviceId => !services.Any(x => x.ServiceId == serviceId))
                .Select(serviceId => CreateGarageService(garage, serviceId))
                .ToList();

            foreach (var id in serviceIdsToDelete)
            {
                var serviceToDelete = services.FirstOrDefault(s => s.ServiceId == id);
                _repository.Delete(serviceToDelete);
            }

            foreach (var service in addedServices)
            {
                await _repository.AddAsync(service);
            }
        }

        private async Task HandleCityAndAddress(ModifyGarageViewModel model, Garage garage)
        {
            var address = await _repository.GetByIdAsync<Address>(garage.AddressId);
            var city = await _repository.GetByIdAsync<City>(address.CityId);

            if (city.Name != model.City)
            {
                if (await _repository.AllReadonly<City>().AnyAsync(x => x.Name == model.City))
                {
                    city = await _repository.All<City>().FirstOrDefaultAsync(x => x.Name == model.City);
                }
                else
                {
                    City newCity = CreateCity(model.City);
                    await _repository.AddAsync(newCity);
                    city = newCity;
                }
            }

            if (address.StreetName != model.StreetName)
            {
                address.IsDeleted = true;
                address.DeletedOn = DateTime.UtcNow;
                Address newAddress = CreateAddress(city!, model.StreetName, model.StreetNumber);
                await _repository.AddAsync(newAddress);
                address = newAddress;
            }

            garage.Address = address;
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
                .Where(x => x.GarageId == garageId)
                .Select(x => new GarageServicesModel()
                {
                    Id = x.Service.Id,
                    Name = x.Service.Name
                }).ToListAsync();

            return services;
        }

        public async Task<GarageViewModel> GetGarageByIdAsync(int id)
        {
            GarageViewModel garage = await _repository.AllReadonly<Garage>()
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
                    Category = g.Category.Name,
                    NoteTitle = g.Note.Title,
                    NoteDescription = g.Note.Description,
                    NoteImageUrl = g.Note.ImageUrl,
                    NoteIsVisible = g.Note.Vissible
                    

                })
                .FirstAsync();

            return garage!;
        }

        public async Task<ModifyGarageViewModel> ModifyGarageByIdAsync(int id)
        {
            ModifyGarageViewModel? garage = await _repository.AllReadonly<Garage>()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(x => new ModifyGarageViewModel()
                {
                    Id = x.Id,
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

        public async Task Delete(int id)
        {
            var car = await _repository.GetByIdAsync<Garage>(id);
            car.IsDeleted = true;
            car.DeletedOn = DateTime.Now;

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(Delete), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }

        public async Task CreateNoteAsync(AddNoteViewModel model)
        {
            /*Note note = new Note()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Vissible = true
            };

            *//*try
            {
                await _repository.AddAsync(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(Delete), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }*/

            foreach (var garageId in model.GarageIds)
            {
                var garage = await _repository.All<Garage>()
                    .Include(x => x.Note)
                    .Where(x => x.Id == garageId)
                    .FirstAsync();
                    
                garage.Note = new Note()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Vissible = true
                };

            }

            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(Delete), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }
    }
}
