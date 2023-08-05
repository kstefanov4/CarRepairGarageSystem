namespace CarRepairGarage.Web.ViewModels.Garage
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using CarRepairGarage.Common;

    /// <summary>
    /// Represents a view model for modifying a garage's details.
    /// </summary>
    public class ModifyGarageViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the garage.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the garage.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageNameMinLenght)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the image file for the garage.
        /// </summary>
        public IFormFile? Image { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category for the garage.
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
        /// Gets or sets the street name where the garage is located.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageStreetNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageStreetNameLenght)]
        public string StreetName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the street number where the garage is located.
        /// </summary>
        [Required]
        public int StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of service IDs offered by the garage.
        /// </summary>
        public List<int> Services { get; set; } = new List<int>();
    }
}
