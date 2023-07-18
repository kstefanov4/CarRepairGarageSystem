using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Car
{
    public class CarDetailsViewModel
    {
        public string Make { get; set; } = null!;

        public string CarModel { get; set; } = null!;

        public int Year { get; set; }
    }
}
