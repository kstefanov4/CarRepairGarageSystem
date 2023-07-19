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
        [Display(Name = "Appointment Date")]
        public DateTime SelectedDate { get; set; }

        [Required]
        [Display(Name = "Appointment Time")]
        public DateTime SelectedTime { get; set; }

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
