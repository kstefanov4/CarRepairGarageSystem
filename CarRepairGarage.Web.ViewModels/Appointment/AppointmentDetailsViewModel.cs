namespace CarRepairGarage.Web.ViewModels.Appointment
{
    /// <summary>
    /// View model representing the details of an appointment.
    /// </summary>
    public class AppointmentDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the appointment ID.
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or sets the selected date for the appointment.
        /// </summary>
        public string SelectedDate { get; set; } = null!;

        /// <summary>
        /// Gets or sets the selected time for the appointment.
        /// </summary>
        public string SelectedTime { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the garage associated with the appointment.
        /// </summary>
        public int GarageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the garage associated with the appointment.
        /// </summary>
        public string GarageName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the service associated with the appointment.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the service associated with the appointment.
        /// </summary>
        public string ServiceName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the car associated with the appointment.
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Gets or sets the VIN (Vehicle Identification Number) of the car associated with the appointment.
        /// </summary>
        public string CarVIN { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the appointment is approved.
        /// </summary>
        public bool? IsApproved { get; set; }
    }
}