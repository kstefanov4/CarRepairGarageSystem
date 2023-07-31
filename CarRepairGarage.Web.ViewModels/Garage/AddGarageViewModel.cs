namespace CarRepairGarage.Web.ViewModels.Garage
{
    using System.ComponentModel.DataAnnotations;

    using CarRepairGarage.Common;

    public class AddGarageViewModel
    {
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageNameMinLenght)]
        public string Name { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageCityMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageCityMinLenght)]
        public string City { get; set; } = null!;
        
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.GarageStreetNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.GarageStreetNameLenght)]
        public string StreetName { get; set; } = null!;

        [Required]
        public int StreetNumber { get; set; }

        public List<int> ServiceIds { get; set; } = new List<int>();
    }
}
