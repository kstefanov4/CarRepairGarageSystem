namespace CarRepairGarage.Web.ViewModels.Appointment
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAppointmentModel
    {
        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime SelectedDate { get; set; }

        [Required]
        [Display(Name = "Appointment Time")]
        public DateTime SelectedTime { get; set; }

        [Required]
        [Display(Name = "Garage")]
        public int GarageId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        [Required]
        [Display(Name = "Car")]
        public int CarId { get; set; }
    }
}
