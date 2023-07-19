using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services.Contracts
{
    public interface IAppointmentService
    {
        Task<List<string>> GetAllAvailableHours(DateTime dateTime, int garageId);
    }
}
