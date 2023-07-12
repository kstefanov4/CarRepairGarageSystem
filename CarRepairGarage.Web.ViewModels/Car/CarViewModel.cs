using CarRepairGarage.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRepairGarage.Web.ViewModels.Car
{
    public class CarViewModel
    {
        public int Id { get; set; }

        public string VIN { get; set; } = null!;

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }
    }
}
