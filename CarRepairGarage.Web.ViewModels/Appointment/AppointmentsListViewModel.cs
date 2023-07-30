using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Appointment
{
    public class AppointmentsListViewModel
    {
        public IEnumerable<AppointmentDetailsViewModel> Appointments { get; set; }
    }
}
