namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents a repair service category in the database.
    /// </summary>
    [Comment("Repaire service Category")]
    public class Category : BaseDeletableModel
    {
        public Category()
        {
            Garages = new HashSet<Garage>();
        }

        /// <summary>
        /// Gets or sets the primary key of the category.
        /// </summary>
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required]
        [MaxLength(GeneralApplicationConstants.Validations.CategoryNameMaxLenght)]
        [Comment("Category Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the category.
        /// </summary>
        [Required]
        [MaxLength(GeneralApplicationConstants.Validations.CategoryDescriptionMaxLenght)]
        [Comment("Category Description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the image associated with the category.
        /// </summary>
        [Required]
        [Comment("Category Image")]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of garages that belong to this category.
        /// </summary>
        public virtual ICollection<Garage> Garages { get; set; }
    }
}
