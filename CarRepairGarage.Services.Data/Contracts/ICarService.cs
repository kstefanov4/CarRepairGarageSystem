using CarRepairGarage.Web.ViewModels.Car;

namespace CarRepairGarage.Services.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarViewModel>> GetAllCarsByIdAsync(Guid id);
    }
}
