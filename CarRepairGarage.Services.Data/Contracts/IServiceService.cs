namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Service;
    
    public interface IServiceService
    {
        Task<List<ServiceViewModel>> GetAllServiceAsync();
        Task<IEnumerable<string>> AllServicesNameAsync();
        Task<string> GetServiceByIdAsync(int id);
    }
}
