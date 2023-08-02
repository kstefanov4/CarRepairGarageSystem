namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Appointment;
    using CarRepairGarage.Web.ViewModels.Appointment.Enums;

    /// <summary>
    /// Service class responsible for managing appointments.
    /// </summary>
    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService"/> class.
        /// </summary>
        /// <param name="repository">The repository used for data access.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public AppointmentService(
            IRepository repository,
            ILogger<AppointmentService> logger) : base(logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new appointment for a user.
        /// </summary>
        /// <param name="model">The appointment details to create.</param>
        /// <param name="user">The application user for whom the appointment is created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAppointmentAsync(CreateAppointmentModel model, ApplicationUser user)
        {
            Appointment appointment = new Appointment()
            {
                User = user,
                ServiceId = model.ServiceId,
                GarageId = model.GarageId,
                CarId = model.CarId,
                Date = model.SelectedDate,
                Time = model.SelectedTime.TimeOfDay
            };

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.AddAsync(appointment);
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Delete(Guid id)
        {
            //TODO check if the user is the owner

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.DeleteAsync<Appointment>(id.ToString());
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Checks if an appointment with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the appointment to check.</param>
        /// <returns><c>true</c> if the appointment exists; otherwise, <c>false</c>.</returns>
        public async Task<bool> Exist(Guid id)
        {
            return await _repository.AllReadonly<Appointment>()
                .AnyAsync(x => x.Id == id.ToString());
        }

        /// <summary>
        /// Retrieves all appointments for a user with optional filtering and pagination.
        /// </summary>
        /// <param name="queryModel">The query model containing filter and pagination information.</param>
        /// <param name="id">The ID of the user.</param>
        /// <returns>A model containing the filtered and paginated appointments.</returns>
        public async Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByUserIdAsync(AllAppointmentsQueryModel queryModel, Guid id)
        {
            IQueryable<Appointment> appointmentQuery = _repository.All<Appointment>()
                .Include(x => x.Garage)
                .Where(x => x.UserId == id && x.Garage.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Approved")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == true && x.Date.DayOfYear >= DateTime.Now.DayOfYear);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Rejected")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == false && x.Date.DayOfYear >= DateTime.Now.DayOfYear);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Pending")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == null && x.Date.DayOfYear >= DateTime.Now.DayOfYear);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Expired")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Date.DayOfYear < DateTime.Now.DayOfYear);
            }
            // Sorting
            switch (queryModel.AppointmentSorting)
            {
                case AppointmentSorting.Newest:
                    appointmentQuery = appointmentQuery.OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);
                    break;
                case AppointmentSorting.Oldest:
                    appointmentQuery = appointmentQuery.OrderBy(x => x.Date).ThenBy(x => x.Time);
                    break;
                default:
                    appointmentQuery = appointmentQuery.OrderByDescending(x => x.Id);
                    break;
            }

            // Total count of filtered garages
            int totalAppointments = appointmentQuery.Count();

            // Pagination
            appointmentQuery = appointmentQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.AppointmentsPerPage)
                .Take(queryModel.AppointmentsPerPage);

            IEnumerable<AppointmentDetailsViewModel> allGarages = await appointmentQuery
                .Select(x => new AppointmentDetailsViewModel()
                {
                    Id = x.Id.ToString(),
                    GarageName = x.Garage.Name,
                    GarageId = x.GarageId,
                    CarId = x.CarId,
                    CarVIN = x.Car.VIN,
                    ServiceId = x.ServiceId,
                    ServiceName = x.Service.Name,
                    SelectedDate = x.Date.ToString(@"yyyy-MM-dd"),
                    SelectedTime = x.Time.ToString(@"hh\:mm"),
                    IsApproved = x.Confirmed
                })
                .ToArrayAsync();

            return new AllAppointmentsFilteredAndPagedServiceModel()
            {
                TotalAppointmentCount = totalAppointments,
                Appointments = allGarages
            };
        }

        /// <summary>
        /// Retrieves all appointments for a garage with optional filtering and pagination.
        /// </summary>
        /// <param name="queryModel">The query model containing filter and pagination information.</param>
        /// <param name="id">The ID of the garage.</param>
        /// <returns>A model containing the filtered and paginated appointments.</returns>
        public async Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByGarageIdAsync(AllAppointmentsQueryModel queryModel, Guid id)
        {
            IQueryable<Appointment> appointmentQuery = _repository.All<Appointment>()
                .Include(x => x.Garage)
                .Where(x => x.Garage.UserId == id && x.Garage.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Approved")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == true && x.Date.DayOfYear >= DateTime.Now.DayOfYear);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Rejected")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == false && x.Date.DayOfYear >= DateTime.Now.DayOfYear );
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Pending")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == null && x.Date.DayOfYear >= DateTime.Now.DayOfYear);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Expired")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Date.DayOfYear < DateTime.Now.DayOfYear);
            }
            // Sorting
            switch (queryModel.AppointmentSorting)
            {
                case AppointmentSorting.Newest:
                    appointmentQuery = appointmentQuery.OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);
                    break;
                case AppointmentSorting.Oldest:
                    appointmentQuery = appointmentQuery.OrderBy(x => x.Date).ThenBy(x => x.Time);
                    break;
                default:
                    appointmentQuery = appointmentQuery.OrderByDescending(x => x.Id);
                    break;
            }

            // Total count of filtered garages
            int totalAppointments = appointmentQuery.Count();

            // Pagination
            appointmentQuery = appointmentQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.AppointmentsPerPage)
                .Take(queryModel.AppointmentsPerPage);

            IEnumerable<AppointmentDetailsViewModel> allGarages = await appointmentQuery
                .Select(x => new AppointmentDetailsViewModel()
                {
                    Id = x.Id.ToString(),
                    GarageName = x.Garage.Name,
                    GarageId = x.GarageId,
                    CarId = x.CarId,
                    CarVIN = x.Car.VIN,
                    ServiceId = x.ServiceId,
                    ServiceName = x.Service.Name,
                    SelectedDate = x.Date.ToString(@"yyyy-MM-dd"),
                    SelectedTime = x.Time.ToString(@"hh\:mm"),
                    IsApproved = x.Confirmed
                })
                .ToArrayAsync();

            return new AllAppointmentsFilteredAndPagedServiceModel()
            {
                TotalAppointmentCount = totalAppointments,
                Appointments = allGarages
            };
        }

        /// <summary>
        /// Retrieves all appointments for a garage without filtering and pagination.
        /// </summary>
        /// <param name="id">The ID of the garage.</param>
        /// <returns>A collection of appointment view models.</returns>
        public async Task<IEnumerable<AppointmentDetailsViewModel>> GetAllAppointmentsByGarageIdAsync(int id)
        {
            var appointments = await _repository.AllReadonly<Appointment>()
                .Where(a => a.Date >= DateTime.UtcNow)
                .Include(x => x.Garage)
                .Where(x => x.Garage.Id == id && x.Garage.IsDeleted == false)
                .Select(x => new AppointmentDetailsViewModel()
                {
                    Id = x.Id.ToString(),
                    GarageName = x.Garage.Name,
                    GarageId = x.GarageId,
                    ServiceId = x.ServiceId,
                    CarVIN = x.Car.VIN,
                    ServiceName = x.Service.Name,
                    SelectedDate = x.Date.ToString(@"yyyy-MM-dd"),
                    SelectedTime = x.Time.ToString(@"hh\:mm"),
                    IsApproved = x.Confirmed
                })
                .ToListAsync();

            return appointments;
        }

        /// <summary>
        /// Retrieves all available appointment hours for a specific garage and service on a given date.
        /// </summary>
        /// <param name="dateTime">The date for which to check the available hours.</param>
        /// <param name="garageId">The ID of the garage.</param>
        /// <param name="serviceId">The ID of the service.</param>
        /// <returns>A list of available appointment hours.</returns>
        public async Task<List<string>> GetAllAvailableHours(DateTime dateTime, int garageId, int serviceId)
        {
            List<string> hours = await _repository.AllReadonly<Appointment>()
                .Where(x => x.Date == dateTime && x.GarageId == garageId && x.ServiceId == serviceId)
                .Select(x => x.Time.ToString().Substring(0,5))
                .ToListAsync();
            
            return hours;
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>An appointment model containing the details of the appointment.</returns>
        public async Task<AppointmentModel> GetAppointmentByIdAsync(string id)
        {
            var appointment = await _repository.GetByIdAsync<Appointment>(id);
            var garageOwner = await _repository.AllReadonly<Garage>()
                .Where(x => x.Id == appointment.GarageId)
                .Select(x => x.UserId)
                .FirstAsync();

            return new AppointmentModel()
            {
                GarageId = appointment.GarageId,
                GarageOwner = garageOwner.ToString(),
                SelectedDate = appointment.Date.ToString(@"yyyy-MM-dd"),
                SelectedTime = appointment.Time.ToString(@"hh\:mm"),
                ServiceId = appointment.ServiceId,
                UserId = appointment.UserId,
                Approved = appointment.Confirmed

            };
        }

        /// <summary>
        /// Approves an appointment by changing its confirmation status to true.
        /// </summary>
        /// <param name="id">The ID of the appointment to approve.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Approve(string id)
        {
            var appointent = await _repository.All<Appointment>()
                .Where(x => x.Id == id)
                .FirstAsync();

            appointent.Confirmed = true;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Rejects an appointment by changing its confirmation status to false.
        /// </summary>
        /// <param name="id">The ID of the appointment to reject.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Reject(string id)
        {
            var appointent = await _repository.All<Appointment>()
                .Where(x => x.Id == id)
                .FirstAsync();

            appointent.Confirmed = false;

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.SaveChangesAsync();
            });
        }
    }
}
