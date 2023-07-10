namespace CarRepairGarage.Services.Data.Garage.Contracts
{
    using CarRepairGarage.Web.ViewModels.Garage;
    public interface IGarageService
    {
        Task<IEnumerable<GarageViewModel>> GetAllGaragesAsync();
    }
}
