namespace CarRepairGarage.Web.Components.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    
    using CarRepairGarage.Services.Contracts;
    using CarRepairGarage.Web.ViewModels.Appointment;

    public class AllAppointmentsByGarageViewComponent : ViewComponent
    {
        private readonly IAppointmentService _appointmentService;

        public AllAppointmentsByGarageViewComponent(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var viewModel = new AppointmentsListViewModel
            {
                Appointments =
                    await _appointmentService.GetAllAppointmentsByGarageIdAsync(id),
            };

            return View(viewModel);
        }
    }
}
