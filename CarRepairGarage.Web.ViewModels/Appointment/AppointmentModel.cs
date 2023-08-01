namespace CarRepairGarage.Web.ViewModels.Appointment
{
    /// <summary>
    /// View model representing an appointment.
    /// </summary>
    public class AppointmentModel
    {
        /// <summary>
        /// Gets or sets the selected date for the appointment.
        /// </summary>
        public string SelectedDate { get; set; } = null!;

        /// <summary>
        /// Gets or sets the selected time for the appointment.
        /// </summary>
        public string SelectedTime { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user ID associated with the appointment.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the garage associated with the appointment.
        /// </summary>
        public int GarageId { get; set; }

        /// <summary>
        /// Gets or sets the owner of the garage associated with the appointment.
        /// </summary>
        public string? GarageOwner { get; set; }

        /// <summary>
        /// Gets or sets the ID of the service associated with the appointment.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets whether the appointment is approved.
        /// </summary>
        public bool? Approved { get; set; }
    }
}
