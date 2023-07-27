using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRepairGarage.Web.ViewModels.Appointment
{
    public class AppointmentModel
    {
        public string SelectedDate { get; set; } = null!;
        public string SelectedTime { get; set; } = null!;
        public Guid UserId { get; set; }
        public int GarageId { get; set; }
        public string? GarageOwner { get; set; }
        public int ServiceId { get; set; }
        public bool? Approved { get; set; }
    }
}
