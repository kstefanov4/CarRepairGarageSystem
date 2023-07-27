using CarRepairGarage.Web.ViewModels.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class ModifyGarageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int CategoryId { get; set; }

        public string City { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public int StreetNumber { get; set; }

        public List<int> Services { get; set; } = new List<int>();
    }
}
