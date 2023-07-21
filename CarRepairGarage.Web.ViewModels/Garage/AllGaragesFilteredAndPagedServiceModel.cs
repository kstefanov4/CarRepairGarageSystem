using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class AllGaragesFilteredAndPagedServiceModel
    {
        public AllGaragesFilteredAndPagedServiceModel()
        {
            this.Garages = new HashSet<GarageViewModel>();
        }
        public int TotalGarageCount { get; set; }
        public IEnumerable<GarageViewModel> Garages { get; set; }
    }
}
