namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Garage;
    public interface IGarageService
    {
        Task<IEnumerable<GarageViewModel>> GetAllGaragesAsync(int count);
        Task<List<GarageServicesModel>> GetAllServicesByGarageIdAsync(int garageId);
        Task<AllGaragesFilteredAndPagedServiceModel> AllAsync(AllGaragesQueryModel queryModel);
    }
}
