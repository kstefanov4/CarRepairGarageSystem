namespace CarRepairGarage.Web.Components.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Appointment;

    /// <summary>
    /// View component for displaying all appointments by garage.
    /// </summary>
    public class AllAppointmentsByGarageViewComponent : ViewComponent
    {
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllAppointmentsByGarageViewComponent"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service.</param>
        public AllAppointmentsByGarageViewComponent(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Method that invokes the view component asynchronously to render the view.
        /// </summary>
        /// <param name="id">The ID of the garage to fetch appointments for.</param>
        /// <returns>The view component result representing the appointments list view model.</returns>
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var viewModel = new AppointmentsListViewModel
            {
                Appointments = await _appointmentService.GetAllAppointmentsByGarageIdAsync(id),
            };

            return View(viewModel);
        }
    }

}
