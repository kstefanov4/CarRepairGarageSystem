namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Garage;
    using CarRepairGarage.Web.ViewModels.Note;

    public interface IGarageService
    {
        Task<IEnumerable<GarageViewModel>> GetAllGaragesAsync(int count);
        Task<List<GarageServicesModel>> GetAllServicesByGarageIdAsync(int garageId);
        Task<AllGaragesFilteredAndPagedServiceModel> AllAsync(AllGaragesQueryModel queryModel);
        Task<GarageViewModel> GetGarageByIdAsync(int id);
        Task<ModifyGarageViewModel> ModifyGarageByIdAsync(int id);
        Task<IEnumerable<GarageViewModel>> GetAllGaragesByOwnerAsync(string id);
        Task AddGarageAsync(AddGarageViewModel model, ApplicationUser user);
        Task<bool> Exists(int id);
        Task Edit(ModifyGarageViewModel model);
        Task Delete(int id);
        
    }
}
