namespace CarRepairGarage.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;
    
    using CarRepairGarage.Common;

    /// <summary>
    /// View model representing the data required to add a new car.
    /// </summary>
    public class AddCarViewModel
    {
        /// <summary>
        /// Gets or sets the Vehicle Identification Number (VIN) of the car.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarVINMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CarVINMinLenght)]
        public string VIN { get; set; } = null!;

        /// <summary>
        /// Gets or sets the make of the car.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarMakeMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CarMakeMinLenght)]
        public string Make { get; set; } = null!;

        /// <summary>
        /// Gets or sets the model of the car.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarModelMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CarModelMinLenght)]
        public string CarModel { get; set; } = null!;

        /// <summary>
        /// Gets or sets the year of the car.
        /// </summary>
        [Required]
        [Range(GeneralApplicationConstants.Validations.CarYearMinLenght, GeneralApplicationConstants.Validations.CarYearMaxYear)]
        public int Year { get; set; }
    }
}