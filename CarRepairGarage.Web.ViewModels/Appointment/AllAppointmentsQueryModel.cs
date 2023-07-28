using CarRepairGarage.Web.ViewModels.Garage.Enums;
using CarRepairGarage.Web.ViewModels.Garage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CarRepairGarage.Web.ViewModels.Appointment.Enums;

namespace CarRepairGarage.Web.ViewModels.Appointment
{
    public class AllAppointmentsQueryModel
    {
        public AllAppointmentsQueryModel()
        {
            this.CurrentPage = 1;
            this.AppointmentsPerPage = 6;

            this.Statuses = new List<string>()
            {
                "Approved",
                "Rejected",
                "Pending",
                "Expired"
            };

            Appointments = new HashSet<AppointmentDetailsViewModel>();
        }
        public string? Status { get; set; }

        [Display(Name = "Search")]
        public string? SearchByString { get; set; }

        [Display(Name = "Sort By")]
        public AppointmentSorting AppointmentSorting { get; set; }
        public int CurrentPage { get; set; }
        public int AppointmentsPerPage { get; set; }
        public int TotalGarages { get; set; }
        public IEnumerable<string> Statuses { get; set; } = null!;
        public IEnumerable<AppointmentDetailsViewModel> Appointments { get; set; }
    }
}
