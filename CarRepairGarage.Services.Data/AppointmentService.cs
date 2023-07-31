﻿namespace CarRepairGarage.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Appointment;
    using CarRepairGarage.Web.ViewModels.Appointment.Enums;

    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IRepository _repository;

        public AppointmentService(
            IRepository repository,
            ILogger<AppointmentService> logger) : base(logger)
        {
            _repository = repository;
        }

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

        public async Task Delete(Guid id)
        {
            //TODO check if the user is the owner

            await ExecuteDatabaseAction(async () =>
            {
                await _repository.DeleteAsync<Appointment>(id.ToString());
                await _repository.SaveChangesAsync();
            });
        }

        public async Task<bool> Exist(Guid id)
        {
            return await _repository.AllReadonly<Appointment>()
                .AnyAsync(x => x.Id == id.ToString());
        }

        public async Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByUserIdAsync(AllAppointmentsQueryModel queryModel, Guid id)
        {
            IQueryable<Appointment> appointmentQuery = _repository.All<Appointment>()
                .Include(x => x.Garage)
                .Where(x => x.UserId == id && x.Garage.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Approved")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == true);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Rejected")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == false);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Pending")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == null && x.Date >= DateTime.UtcNow);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Expired")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Date < DateTime.UtcNow);
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

        public async Task<AllAppointmentsFilteredAndPagedServiceModel> GetAllAppointmentsByGarageIdAsync(AllAppointmentsQueryModel queryModel, Guid id)
        {
            IQueryable<Appointment> appointmentQuery = _repository.All<Appointment>()
                .Include(x => x.Garage)
                .Where(x => x.Garage.UserId == id && x.Garage.IsDeleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Approved")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == true);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Rejected")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == false);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Pending")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Confirmed == null && x.Date >= DateTime.UtcNow);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Status) && queryModel.Status == "Expired")
            {
                appointmentQuery = appointmentQuery.Where(x => x.Date < DateTime.UtcNow);
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

        public async Task<IEnumerable<AppointmentDetailsViewModel>> GetAllAppointmentsByGarageIdAsync(int id)
        {
            var appointments = await _repository.AllReadonly<Appointment>()
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

        public async Task<List<string>> GetAllAvailableHours(DateTime dateTime, int garageId, int serviceId)
        {
            List<string> hours = await _repository.AllReadonly<Appointment>()
                .Where(x => x.Date == dateTime && x.GarageId == garageId && x.ServiceId == serviceId)
                .Select(x => x.Time.ToString().Substring(0,5))
                .ToListAsync();
            
            return hours;
        }

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
