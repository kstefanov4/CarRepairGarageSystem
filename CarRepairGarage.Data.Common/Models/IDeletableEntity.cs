namespace CarRepairGarage.Data.Common.Models
{
    using System;

    /// <summary>
    /// Represents an interface for entities that support soft deletion (logical deletion) tracking.
    /// </summary>
    public interface IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was deleted.
        /// </summary>
        DateTime? DeletedOn { get; set; }
    }
}
