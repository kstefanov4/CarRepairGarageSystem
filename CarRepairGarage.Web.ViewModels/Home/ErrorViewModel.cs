namespace CarRepairGarage.Web.ViewModels.Home
{
    /// <summary>
    /// Represents a view model for displaying error information on the home page.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the request ID associated with the error.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the request ID or not.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

}