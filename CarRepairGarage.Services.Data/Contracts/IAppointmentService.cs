using CarRepairGarage.Data.Models;
using CarRepairGarage.Web.ViewModels.Appointment;
using CarRepairGarage.Web.ViewModels.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services.Contracts
{
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
