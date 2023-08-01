namespace CarRepairGarage.Web.ViewModels.Category
{
    using CarRepairGarage.Web.ViewModels.Garage;
    /// <summary>
    /// View model representing a category entity.
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the image associated with the category.
        /// </summary>
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of garage view models associated with the category.
        /// </summary>
        public virtual ICollection<GarageViewModel> Garages { get; set; } = new List<GarageViewModel>();
    }
}
