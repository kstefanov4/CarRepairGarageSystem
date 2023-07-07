namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CarRepairGarage.Data.Common.Models;
    using CarRepairGarage.Common;

    public class Notes : BaseDeletableModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

    }
}
