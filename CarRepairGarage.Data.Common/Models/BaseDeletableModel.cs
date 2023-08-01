namespace CarRepairGarage.Data.Common.Models
{
    using System;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the base model for entities that support soft deletion (logical deletion).
    /// </summary>
    [Comment("BaseDeletableModel")]
    public abstract class BaseDeletableModel : IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was deleted.
        /// </summary>
        public DateTime? DeletedOn { get; set; }
    }
}
