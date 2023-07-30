using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Note
{
    public class AddNoteViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(2550, MinimumLength = 2)]
        public string Description { get; set; } = null!;

        public List<int> GarageIds { get; set; } = new List<int>();
    }
}
