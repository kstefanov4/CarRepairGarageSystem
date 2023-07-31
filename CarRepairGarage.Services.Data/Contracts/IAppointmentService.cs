namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Appointment;

    public interface IAppointmentService
    {
        Task<List<string>> GetAllAvailableHours(DateTime dateTime, int garageId, int serviceId);
        Task CreateAppointmentAsync(CreateAppointmentModel model, ApplicationUser user);
        Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByUserIdAsync(AllAppointmentsQueryModel queryModel, Guid id);
        Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByGarageIdAsync(AllAppointmentsQueryModel queryModel, Guid id);
        Task<IEnumerable<AppointmentDetailsViewModel>> GetAllAppointmentsByGarageIdAsync(int id);
        Task<bool> Exist(Guid id);
        Task<AppointmentModel> GetAppointmentByIdAsync(string id);
        Task Delete(Guid id);
        Task Approve(string id);
        Task Reject(string id);
    }
}
