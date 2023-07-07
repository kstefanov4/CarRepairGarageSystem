namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    [Comment("Repaire service Category")]
    public class Category : BaseDeletableModel
    {
        public Category()
        {
            Services = new HashSet<Service>();
            Garages = new HashSet<Garage>();
        }

        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GeneralApplicationConstants.DataValidations.NameMaxLenght)]
        [Comment("Category Name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(GeneralApplicationConstants.DataValidations.DescriptionMaxLength)]
        [Comment("Category Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Category Image")]
        public string ImageUrl { get; set; } = null!;

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<Garage> Garages { get; set; }
    }
}
