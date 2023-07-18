using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Car
{
    public class AddCarViewModel
    {
        [Required]
        [StringLength(17, MinimumLength = 17)]
        public string VIN { get; set; } = null!;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Make { get; set; } = null!;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string CarModel { get; set; } = null!;
        [Required]
        [Range(1900,2023)]
        public int Year { get; set; }
    }
}
