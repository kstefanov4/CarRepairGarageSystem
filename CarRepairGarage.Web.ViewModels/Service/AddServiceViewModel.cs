namespace CarRepairGarage.Web.ViewModels.Service
{
    using System.ComponentModel.DataAnnotations;
    using CarRepairGarage.Common;
    public class AddServiceViewModel
    {
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.ServiceNameMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.ServiceNameMinLenght)]
        public string Name { get; set; } = null!;

        [MaxLength(GeneralApplicationConstants.Validations.ServiceDescriptionMaxLenght)]
        public string? Description { get; set; }
    }
}
