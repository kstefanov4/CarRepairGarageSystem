using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRepairGarage.Web.ViewModels.Appointment
{
    public class CreateAppointmentViewModel
    {
        [Required]
        [Display(Name = "Appointment Date and Time")]
        public DateTime SelectedDateTime { get; set; }

        [Required]
        [Display(Name = "User")]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Garage")]
        public int GarageId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        public List<DateTime> AvailableDates { get; set; }
    }
}
