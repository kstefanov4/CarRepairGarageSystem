namespace CarRepairGarage.Web.ViewModels.Garage
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using CarRepairGarage.Common;

    /// <summary>
    /// Represents the data required to add a new garage in the application.
    /// </summary>
    public class AddGarageViewModel
    {
        /// <summary>
        /// Gets or sets the name of the garage.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageNameMinLenght)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the image of the garage.
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Gets or sets the category ID of the garage.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the city where the garage is located.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageCityMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageCityMinLenght)]
        public string City { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the street where the garage is located.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageStreetNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageStreetNameLenght)]
        public string StreetName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the street number of the garage.
        /// </summary>
        [Required]
        public int StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of service IDs associated with the garage.
        /// </summary>
        public List<int> ServiceIds { get; set; } = new List<int>();
    }
}
