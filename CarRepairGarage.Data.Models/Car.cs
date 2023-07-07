namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    [Comment("User Car")]
    public class Car : BaseDeletableModel
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [Comment("Car VIN number")]
        public string VIN { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.CarMakeMaxLenght)]
        [Comment("Car Make")]
        public string Make { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.CarMakeMinLenght)]
        [Comment("Primary Model")]
        public string Model { get; set; } = null!;

        [Required]
        [Comment("Car year of production")]
        public int Year { get; set; }

        [Required]
        [Comment("User of the car")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

    }
}
