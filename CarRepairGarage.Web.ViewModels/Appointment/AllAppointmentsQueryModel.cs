namespace CarRepairGarage.Web.ViewModels.Appointment
{
    using System.ComponentModel.DataAnnotations;

    // Importing the required enum
    using CarRepairGarage.Web.ViewModels.Appointment.Enums;

    /// <summary>
    /// View model representing the query parameters for retrieving all appointments.
    /// </summary>
    public class AllAppointmentsQueryModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllAppointmentsQueryModel"/> class.
        /// </summary>
        public AllAppointmentsQueryModel()
        {
            // Setting default values for properties
            this.CurrentPage = 1;
            this.AppointmentsPerPage = 6;

            this.Statuses = new List<string>()
            {
                "Approved",
                "Rejected",
                "Pending",
                "Expired"
            };

            // Initialize the collection of Appointments with an empty HashSet.
            Appointments = new HashSet<AppointmentDetailsViewModel>();
        }

        /// <summary>
        /// Gets or sets the status of the appointments to filter by.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the search string to filter appointments by.
        /// </summary>
        [Display(Name = "Search")]
        public string? SearchByString { get; set; }

        /// <summary>
        /// Gets or sets the sorting option for appointments.
        /// </summary>
        [Display(Name = "Sort By")]
        public AppointmentSorting AppointmentSorting { get; set; }

        /// <summary>
        /// Gets or sets the current page number for pagination.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of appointments to display per page.
        /// </summary>
        public int AppointmentsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of garages after filtering.
        /// </summary>
        public int TotalGarages { get; set; }

        /// <summary>
        /// Gets or sets the available appointment statuses.
        /// </summary>
        public IEnumerable<string> Statuses { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of appointment details view models.
        /// </summary>
        public IEnumerable<AppointmentDetailsViewModel> Appointments { get; set; }
    }
}
