namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Car;

    public interface ICarService
    {
        Task<IEnumerable<CarViewModel>> GetAllCarsByUserIdAsync(Guid id);
        Task AddCarAsync(AddCarViewModel carModel, ApplicationUser user);
        Task<bool> Exist(int id);
        Task<CarViewModel> GetCarByIdAsync(int id);
        Task Edit(int id, AddCarViewModel model);
        Task Delete(int id);
    }
}
