namespace CarRepairGarage.Web.ViewModels.Appointment
{
    public class AllAppointmentsFilteredAndPagedServiceModel
    {
        public AllAppointmentsFilteredAndPagedServiceModel()
        {
            this.Appointments = new HashSet<AppointmentDetailsViewModel>();
        }
        public int TotalAppointmentCount { get; set; }
        public IEnumerable<AppointmentDetailsViewModel> Appointments { get; set; }
    }
}
