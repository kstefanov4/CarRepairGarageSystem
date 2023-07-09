namespace CarRepairGarage.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// This is custome role class that works with the defoult ASP.NET Core Identity.
    /// You can add additional info to the build-in users.
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

        [Comment("Is Application role deleted")]
        public bool IsDeleted { get; set; }

        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }
    }
}
