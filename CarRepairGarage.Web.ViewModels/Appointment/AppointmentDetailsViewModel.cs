using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRepairGarage.Web.ViewModels.Appointment
{
    public class AppointmentDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string SelectedDate { get; set; } = null!;
        public string SelectedTime { get; set; } = null!;
        public int GarageId { get; set; }
        public string GarageName { get; set; } = null!;
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public bool? IsApproved { get; set; }

    }
}
