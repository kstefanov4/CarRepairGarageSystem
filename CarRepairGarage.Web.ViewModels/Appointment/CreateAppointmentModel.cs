namespace CarRepairGarage.Web.ViewModels.Appointment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model representing the data required to create a new appointment.
    /// </summary>
    public class CreateAppointmentModel
    {
        /// <summary>
        /// Gets or sets the selected date for the appointment.
        /// </summary>
        [Required]
        [Display(Name = "Appointment Date")]
        public DateTime SelectedDate { get; set; }

        /// <summary>
        /// Gets or sets the selected time for the appointment.
        /// </summary>
        [Required]
        [Display(Name = "Appointment Time")]
        public DateTime SelectedTime { get; set; }

        /// <summary>
        /// Gets or sets the ID of the garage associated with the appointment.
        /// </summary>
        [Required]
        [Display(Name = "Garage")]
        public int GarageId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the service associated with the appointment.
        /// </summary>
        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the car associated with the appointment.
        /// </summary>
        [Required]
        [Display(Name = "Car")]
        public int CarId { get; set; }
    }
}
