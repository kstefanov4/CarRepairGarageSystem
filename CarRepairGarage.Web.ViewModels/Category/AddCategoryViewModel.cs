namespace CarRepairGarage.Web.ViewModels.Category
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;
    
    using CarRepairGarage.Common;
    
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CategoryNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CategoryNameMinLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CategoryDescriptionMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CategoryDescriptionMinLenght)]
        public string Description { get; set; } = null!;

        public IFormFile ImageUrl { get; set; }
    }
}
