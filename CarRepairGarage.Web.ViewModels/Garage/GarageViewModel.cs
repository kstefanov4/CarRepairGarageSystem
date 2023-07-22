using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class GarageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string City { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public ICollection<string> Services { get; set; } = new List<string>();
    }
}
