namespace CarRepairGarage.Web.ViewModels.Car
{
    /// <summary>
    /// View model representing a car entity.
    /// </summary>
    public class CarViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the car.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the VIN (Vehicle Identification Number) of the car.
        /// </summary>
        public string VIN { get; set; } = null!;

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

        /// <summary>
        /// Gets or sets the user ID associated with the car.
        /// This property is nullable as a car may not be associated with a specific user.
        /// </summary>
        public string? UserId { get; set; }
    }
}
