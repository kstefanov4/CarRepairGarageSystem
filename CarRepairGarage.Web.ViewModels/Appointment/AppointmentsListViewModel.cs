namespace CarRepairGarage.Web.ViewModels.Appointment
{
    /// <summary>
    /// View model representing a list of appointments.
    /// </summary>
    public class AppointmentsListViewModel
    {
        /// <summary>
        /// Gets or sets the collection of appointment details view models.
        /// </summary>
        public IEnumerable<AppointmentDetailsViewModel> Appointments { get; set; }
    }
}
