namespace CarRepairGarage.Web.ViewModels.Garage
{
    /// <summary>
    /// Represents a view model for displaying garage details.
    /// </summary>
    public class GarageViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the garage.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the garage.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the garage's image.
        /// </summary>
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the category of the garage.
        /// </summary>
        public string Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the owner of the garage.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the city where the garage is located.
        /// </summary>
        public string City { get; set; } = null!;

        /// <summary>
        /// Gets or sets the street name where the garage is located.
        /// </summary>
        public string StreetName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the street number where the garage is located.
        /// </summary>
        public string StreetNumber { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the associated note for the garage.
        /// </summary>
        public int? NoteId { get; set; }

        /// <summary>
        /// Gets or sets the title of the associated note for the garage.
        /// </summary>
        public string? NoteTitle { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image associated with the note for the garage.
        /// </summary>
        public string? NoteImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the associated note for the garage.
        /// </summary>
        public string? NoteDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the associated note for the garage is visible.
        /// </summary>
        public bool? NoteIsVisible { get; set; }

        /// <summary>
        /// Gets or sets the collection of services offered by the garage.
        /// </summary>
        public ICollection<string> Services { get; set; } = new List<string>();
    }

}
