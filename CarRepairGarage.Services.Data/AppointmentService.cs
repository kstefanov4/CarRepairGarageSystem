using CarRepairGarage.Data.Models;
using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Appointment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        public AppointmentService(
            IRepository repository,
            ILogger<AppointmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateAppointmentAsync(CreateAppointmentViewModel model, ApplicationUser user)
        {
            Appointment appointment = new Appointment();
            appointment.User = user;
            appointment.ServiceId = model.ServiceId;
            appointment.GarageId = model.GarageId;
            appointment.Date = model.SelectedDate;
            appointment.Time = model.SelectedTime.TimeOfDay;

            try
            {
                await _repository.AddAsync(appointment);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(CreateAppointmentAsync), ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }

        public async Task<List<string>> GetAllAvailableHours(DateTime dateTime, int garageId)
        {
            List<string> hours = await _repository.AllReadonly<Appointment>()
                .Where(x => x.Date == dateTime && x.GarageId == garageId)
                .Select(x => x.Time.ToString().Substring(0,5))
                .ToListAsync();
            
            return hours;
        }
    }
}
