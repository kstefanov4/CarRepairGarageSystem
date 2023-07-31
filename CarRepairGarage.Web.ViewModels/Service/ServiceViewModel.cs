
namespace CarRepairGarage.Web.ViewModels.Service
{
    using CarRepairGarage.Web.ViewModels.Garage;
    
    public class ServiceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<GarageViewModel> Garages { get; set; } = new List<GarageViewModel>();
    }
}
