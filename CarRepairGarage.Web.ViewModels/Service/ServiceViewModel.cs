namespace CarRepairGarage.Web.ViewModels.Service
{
    using CarRepairGarage.Web.ViewModels.Garage;

    /// <summary>
    /// Represents a view model for a service.
    /// </summary>
    public class ServiceViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the service.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of garages offering this service.
        /// </summary>
        public virtual ICollection<GarageViewModel> Garages { get; set; } = new List<GarageViewModel>();
    }
}
