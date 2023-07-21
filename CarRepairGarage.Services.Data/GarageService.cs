namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Data.Models;
    using System.Linq;
    using CarRepairGarage.Web.ViewModels.Garage.Enums;

    public class GarageService : IGarageService
    {
        private readonly IRepository _repository;
        public GarageService(IRepository repository)
        {
            _repository = repository;
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
