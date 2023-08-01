namespace CarRepairGarage.Web.ViewModels.Garage
{
    /// <summary>
    /// Represents a model for displaying services offered by a garage.
    /// </summary>
    public class GarageServicesModel
    {
        /// <summary>
        /// Gets or sets the ID of the service.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        public string Name { get; set; } = null!;
    }

}
