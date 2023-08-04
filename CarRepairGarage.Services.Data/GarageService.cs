namespace CarRepairGarage.Services
{

    using System.Linq;

    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Garage.Enums;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Service class for managing garage-related operations.
    /// </summary>

    public class GarageService : BaseService, IGarageService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GarageService"/> class.
        /// </summary>
        /// <param name="repository">The repository for data access.</param>
        /// <param name="logger">The logger for logging.</param>
        public GarageService(
            IRepository repository,
            ILogger<GarageService> logger)
            :base(logger)
        {
            _repository = repository;

        }

        /// <summary>
        /// Adds a new garage to the database with the provided information.
        /// </summary>
        /// <param name="model">The <see cref="AddGarageViewModel"/> containing the garage information.</param>
        /// <param name="user">The application user associated with the garage.</param>
        public async Task AddGarageAsync(AddGarageViewModel model, ApplicationUser user)
        {
            var city = await CreateCity(model.City);
            var address = CreateAddress(city, model.StreetName, model.StreetNumber);
            var garage = await CreateGarageAsync(model, user, address);

            var services = model.ServiceIds
                .Select(serviceId => CreateGarageService(garage, serviceId))
                .ToList();

            await ExecuteDatabaseAction(async () =>
            {
                
                await _repository.AddAsync(garage);
                await _repository.AddRangeAsync(services);

                await _repository.SaveChangesAsync();
            });
        }

        private async Task<City> CreateCity(string cityName)
        {
            if (await _repository.AllReadonly<City>().AnyAsync(x => x.Name == cityName))
            {
                return await _repository.All<City>().FirstAsync(x => x.Name == cityName);
            }
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

        private async Task<Garage> CreateGarageAsync(AddGarageViewModel model, ApplicationUser user, Address address)
        {
            string imageUrl = await GetImagePath(model.Image);

            return new Garage
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                ImageUrl = imageUrl,
                Address = address,
                Owner = user
            };
        }

        private static async Task<string> GetImagePath(IFormFile image)
        {
            string imageUrl = null;
            if (image != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                imageUrl = "/images/" + fileName;
            }

            return imageUrl;
        }

        private Data.Models.GarageService CreateGarageService(Garage garage, int serviceId)
        {
            return new Data.Models.GarageService
            {
                Garage = garage,
                ServiceId = serviceId
            };
        }


        public async Task<AllGaragesFilteredAndPagedServiceModel> AllAsync(AllGaragesQueryModel queryModel)
        {
            IQueryable<Garage> garagesQuery = _repository.All<Garage>().Where(x => x.IsDeleted == false).AsQueryable();

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

        /// <summary>
        /// Edits an existing garage in the database with the provided information.
        /// </summary>
        /// <param name="model">The <see cref="ModifyGarageViewModel"/> containing the modified garage information.</param>
        public async Task Edit(ModifyGarageViewModel model)
        {
            var garage = await _repository.GetByIdAsync<Garage>(model.Id);
            var services = await GetGarageServices(garage.Id);

            await HandleServices(model, garage, services);

            await HandleCityAndAddress(model, garage);

            string imageUrl = await GetImagePath(model.Image);

            garage.Name = model.Name;
            garage.ImageUrl = imageUrl;
            garage.CategoryId = model.CategoryId;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
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
                await ExecuteDatabaseAction(async () =>
                {
                    await _repository.AddAsync(service);
                });
                //await _repository.AddAsync(service);
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
                    City newCity = await CreateCity(model.City);
                    await ExecuteDatabaseAction(async () =>
                    {
                        await _repository.AddAsync(newCity);
                    });
                    
                    city = newCity;
                }
            }

            if (address.StreetName != model.StreetName)
            {
                address.IsDeleted = true;
                address.DeletedOn = DateTime.UtcNow;
                Address newAddress = CreateAddress(city!, model.StreetName, model.StreetNumber);
                await ExecuteDatabaseAction(async () =>
                {
                    await _repository.AddAsync(newAddress);
                });
                
                address = newAddress;
            }

            garage.Address = address;
        }

        /// <summary>
        /// Checks if a garage with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the garage to check.</param>
        /// <returns><c>true</c> if the garage exists; otherwise, <c>false</c>.</returns>
        public async Task<bool> Exists(int id)
        {
            return await _repository.AllReadonly<Garage>()
                .AnyAsync(x => x.Id == id && x.IsDeleted == false);
        }

        /// <summary>
        /// Gets a list of all garages with a maximum count.
        /// </summary>
        /// <param name="count">The maximum number of garages to retrieve.</param>
        /// <returns>A list of <see cref="GarageViewModel"/> containing garage information.</returns>
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
                                ImageUrl = x.ImageUrl,
                                StreetName = x.Address.StreetName,
                                StreetNumber = x.Address.StreetNumber.ToString()
                            }).ToListAsync();
            return garages;
        }

        /// <summary>
        /// Gets a list of all garages owned by the user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user whose garages to retrieve.</param>
        /// <returns>A list of <see cref="GarageViewModel"/> containing garage information.</returns>
        public async Task<IEnumerable<GarageViewModel>> GetAllGaragesByOwnerAsync(string id)
        {
            var garages = await _repository.AllReadonly<Garage>()
                .Where(x => x.UserId.ToString() == id && x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .Select(x => new GarageViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    City = x.Address.City.Name,
                    Services = x.Services.Select(x => x.Service.Name).ToList(),
                    ImageUrl = x.ImageUrl,
                    StreetName = x.Address.StreetName,
                    StreetNumber = x.Address.StreetNumber.ToString(),
                    NoteId = x.Note.Id,
                    NoteTitle = x.Note.Title,
                    NoteDescription = x.Note.Description,
                    NoteImageUrl = x.Note.ImageUrl,
                    NoteIsVisible = x.Note.Vissible
                }).ToListAsync();

            return garages;
        }

        /// <summary>
        /// Gets a list of all services offered by the garage with the specified ID.
        /// </summary>
        /// <param name="garageId">The ID of the garage whose services to retrieve.</param>
        /// <returns>A list of <see cref="GarageServicesModel"/> containing service information.</returns>
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

        /// <summary>
        /// Gets a specific garage by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the garage to retrieve.</param>
        /// <returns>The <see cref="GarageViewModel"/> containing garage information.</returns>
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

        /// <summary>
        /// Gets a <see cref="ModifyGarageViewModel"/> for modifying an existing garage with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the garage to retrieve for modification.</param>
        /// <returns>The <see cref="ModifyGarageViewModel"/> containing garage information for modification.</returns>
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
                    /*Image = x.ImageUrl,*/
                    StreetName = x.Address.StreetName,
                    StreetNumber = x.Address.StreetNumber,
                    Services = x.Services.Select(s => s.Service.Id).ToList()
                }).FirstOrDefaultAsync();

            return garage;
        }

        /// <summary>
        /// Deletes the garage with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the garage to delete.</param>
        public async Task Delete(int id)
        {
            var garage = await _repository.GetByIdAsync<Garage>(id);
            garage.IsDeleted = true;
            garage.DeletedOn = DateTime.Now;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }
    }
}
