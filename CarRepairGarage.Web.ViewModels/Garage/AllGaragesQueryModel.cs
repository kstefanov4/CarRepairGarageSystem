using CarRepairGarage.Web.ViewModels.Garage.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class AllGaragesQueryModel
    {
        public AllGaragesQueryModel()
        {
            this.CurrentPage = 1;
            this.GaragesPerPage = 3;

            this.Categories = new HashSet<string>();
            this.Services = new HashSet<string>();
            this.Garages = new HashSet<GarageViewModel>();
        }
        public string? Category { get; set; }
        public string? Service { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchByString { get; set; }

        [Display(Name = "Sort By")]
        public GarageSorting GarageSorting { get; set; }
        public int CurrentPage { get; set; }
        public int GaragesPerPage { get; set; }
        public int TotalGarages { get; set; }
        public IEnumerable<string> Categories { get; set; } = null!;
        public IEnumerable<string> Services { get; set; } = null!;
        public IEnumerable<GarageViewModel> Garages { get; set; } = null!;
    }
}
