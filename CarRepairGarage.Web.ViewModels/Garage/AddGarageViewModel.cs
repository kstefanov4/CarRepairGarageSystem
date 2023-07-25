using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class AddGarageViewModel
    {
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int CategoryId { get; set; }

        public string City { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public int StreetNumber { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
