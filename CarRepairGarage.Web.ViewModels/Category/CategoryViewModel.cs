using CarRepairGarage.Web.ViewModels.Garage;

namespace CarRepairGarage.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<GarageViewModel> Garages { get; set; } = new List<GarageViewModel>();
    }
}
