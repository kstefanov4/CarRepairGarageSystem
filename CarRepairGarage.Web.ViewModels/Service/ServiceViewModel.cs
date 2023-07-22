using CarRepairGarage.Common;
using CarRepairGarage.Web.ViewModels.Garage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Service
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<GarageViewModel> Garages { get; set; } = new List<GarageViewModel>();
    }
}
