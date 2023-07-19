using CarRepairGarage.Data.Models;
using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository _repository;

        public AppointmentService(IRepository repository)
        {
            _repository = repository;
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
