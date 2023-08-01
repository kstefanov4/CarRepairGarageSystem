namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Web.ViewModels.Appointment;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to appointments in the car repair garage application.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Gets a list of all available appointment hours for a specific date, garage, and service.
        /// </summary>
        Task<List<string>> GetAllAvailableHours(DateTime dateTime, int garageId, int serviceId);

        /// <summary>
        /// Creates a new appointment using the provided data.
        /// </summary>
        Task CreateAppointmentAsync(CreateAppointmentModel model, ApplicationUser user);

        /// <summary>
        /// Gets all appointments for a specific user, filtered and paged based on the query model.
        /// </summary>
        Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByUserIdAsync(AllAppointmentsQueryModel queryModel, Guid id);

        /// <summary>
        /// Gets all appointments for a specific garage, filtered and paged based on the query model.
        /// </summary>
        Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByGarageIdAsync(AllAppointmentsQueryModel queryModel, Guid id);

        /// <summary>
        /// Gets all appointments for a specific garage.
        /// </summary>
        Task<IEnumerable<AppointmentDetailsViewModel>> GetAllAppointmentsByGarageIdAsync(int id);

        /// <summary>
        /// Checks if an appointment with the specified ID exists.
        /// </summary>
        Task<bool> Exist(Guid id);

        /// <summary>
        /// Gets an appointment by its ID.
        /// </summary>
        Task<AppointmentModel> GetAppointmentByIdAsync(string id);

        /// <summary>
        /// Deletes an appointment with the specified ID.
        /// </summary>
        Task Delete(Guid id);

        /// <summary>
        /// Approves an appointment with the specified ID.
        /// </summary>
        Task Approve(string id);

        /// <summary>
        /// Rejects an appointment with the specified ID.
        /// </summary>
        Task Reject(string id);
    }
}
