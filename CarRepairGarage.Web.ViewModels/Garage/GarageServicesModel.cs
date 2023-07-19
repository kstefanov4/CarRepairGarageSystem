using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class GarageServicesModel
    {
        public ICollection<string> Services { get; set; } = new List<string>();
    }
}
