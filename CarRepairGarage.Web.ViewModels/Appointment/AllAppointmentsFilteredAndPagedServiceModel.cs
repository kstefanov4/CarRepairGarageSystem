namespace CarRepairGarage.Web.ViewModels.Appointment
{
    /// <summary>
    /// View model representing a collection of filtered and paged appointments.
    /// </summary>
    public class AllAppointmentsFilteredAndPagedServiceModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllAppointmentsFilteredAndPagedServiceModel"/> class.
        /// </summary>
        public AllAppointmentsFilteredAndPagedServiceModel()
        {
            this.Appointments = new HashSet<AppointmentDetailsViewModel>();
        }

        /// <summary>
        /// Gets or sets the total count of appointments after filtering.
        /// </summary>
        public int TotalAppointmentCount { get; set; }

        /// <summary>
        /// Gets or sets the collection of filtered and paged appointment details view models.
        /// </summary>
        public IEnumerable<AppointmentDetailsViewModel> Appointments { get; set; }
    }
}
