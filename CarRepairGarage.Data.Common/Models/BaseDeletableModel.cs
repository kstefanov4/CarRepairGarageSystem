namespace CarRepairGarage.Data.Common.Models
{
    using System;

    using Microsoft.EntityFrameworkCore;

    [Comment("BaseDeletableModel")]
    public abstract class BaseDeletableModel : IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
