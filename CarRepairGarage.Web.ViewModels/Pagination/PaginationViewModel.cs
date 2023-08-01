namespace CarRepairGarage.Web.ViewModels.Pagination
{
    /// <summary>
    /// Represents a view model for handling pagination.
    /// </summary>
    public class PaginationViewModel
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the area name for the pagination link.
        /// </summary>
        public string Area { get; set; } = null!;

        /// <summary>
        /// Gets or sets the controller name for the pagination link.
        /// </summary>
        public string Controller { get; set; } = null!;

        /// <summary>
        /// Gets or sets the action name for the pagination link.
        /// </summary>
        public string Action { get; set; } = null!;
    }
}
