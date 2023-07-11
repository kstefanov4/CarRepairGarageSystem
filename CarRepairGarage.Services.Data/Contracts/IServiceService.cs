namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Service;
    
    public interface IServiceService
    {
        Task<List<ServiceViewModel>> GetAllServiceAsync();
    }
}
