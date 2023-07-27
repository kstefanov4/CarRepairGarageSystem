using CarRepairGarage.Web.ViewModels.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class ModifyGarageViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(1, 3)]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string StreetName { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        public List<int> Services { get; set; } = new List<int>();
    }
}
