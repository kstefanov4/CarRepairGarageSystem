namespace CarRepairGarage.Common
{
    /// <summary>
    /// Contains constants for notification message types in the application.
    /// </summary>
    public static class NotificationsMessagesConstants
    {
        /// <summary>
        /// The key used to store error messages in TempData or ViewData.
        /// </summary>
        public const string ErrorMessage = "ErrorMessage";

        /// <summary>
        /// The key used to store warning messages in TempData or ViewData.
        /// </summary>
        public const string WarningMessage = "WarningMessage";

        /// <summary>
        /// The key used to store information messages in TempData or ViewData.
        /// </summary>
        public const string InformationMessage = "InfoMessage";

        /// <summary>
        /// The key used to store success messages in TempData or ViewData.
        /// </summary>
        public const string SuccessMessage = "SuccessMessage";
    }

}
