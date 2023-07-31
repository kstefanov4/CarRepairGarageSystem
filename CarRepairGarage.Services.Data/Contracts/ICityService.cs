namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    
    public interface ICityService
    {
        Task<IEnumerable<string>> AllCitiesNameAsync();
        Task<bool> Exist(string name);
        Task<City> GetCityByNameAsync(string name);
    }
}
