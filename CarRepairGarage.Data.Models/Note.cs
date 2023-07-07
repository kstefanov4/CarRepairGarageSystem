namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Common.Models;
    using CarRepairGarage.Common;

    [Comment("Garage Notes")]
    public class Note : BaseDeletableModel
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.TitleMaxLength)]
        [Comment("Note Title")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.DescriptionMaxLength)]
        [Comment("Note description")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Note Image")]
        public string ImageUrl { get; set; } = null!;

    }
}
