namespace CarRepairGarage.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents an extended Identity role that works with the default ASP.NET Core Identity.
    /// Allows adding additional information to the built-in roles.
    /// </summary>

    [Comment("Extended Identity Role")]
    public class ApplicationRole : IdentityRole<Guid>, IDeletableEntity
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            
        }

        /// <summary>
        /// Gets or sets a value indicating whether the application role is deleted.
        /// </summary>
        [Comment("Is Application role deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date of deletion of the application role.
        /// </summary>
        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }
    }
}
