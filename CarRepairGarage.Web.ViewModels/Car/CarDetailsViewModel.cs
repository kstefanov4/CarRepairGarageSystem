namespace CarRepairGarage.Web.ViewModels.Car
{
    /// <summary>
    /// View model representing the details of a car.
    /// </summary>
    public class CarDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the make of the car.
        /// </summary>
        public string Make { get; set; } = null!;

        /// <summary>
        /// Gets or sets the model of the car.
        /// </summary>
        public string CarModel { get; set; } = null!;

        /// <summary>
        /// Gets or sets the year of the car.
        /// </summary>
        public int Year { get; set; }
    }
}
