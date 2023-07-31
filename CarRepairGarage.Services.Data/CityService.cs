namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;

    public class CityService : ICityService
    {
        private readonly IRepository _repository;

        public CityService(IRepository repository)
        {
            this._repository = repository;
        }
        public async Task<IEnumerable<string>> AllCitiesNameAsync()
        {
            return await _repository.AllReadonly<City>()
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Name)
                .ToListAsync();
        }

        public async Task<bool> Exist(string name)
        {
            return await _repository.AllReadonly<City>()
                .AnyAsync(x => x.Name == name);
        }

        public async Task<City> GetCityByNameAsync(string name)
        {
            return await _repository.AllReadonly<City>()
                .Where (x => x.Name == name).FirstAsync();
        }
    }
}
