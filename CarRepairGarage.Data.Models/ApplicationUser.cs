namespace CarRepairGarage.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents an extended Identity user that works with the default ASP.NET Core Identity.
    /// Allows adding additional information to the built-in users.
    /// </summary>

    [Comment("Extended Identity User")]
    public class ApplicationUser : IdentityUser<Guid>, IDeletableEntity
    {
        public ApplicationUser()
        {
            //Id = Guid.NewGuid().ToString();
            Roles = new HashSet<IdentityUserRole<Guid>>();
            Appointments = new HashSet<Appointment>();
            Garages = new HashSet<Garage>();
            Cars = new HashSet<Car>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is deleted.
        /// </summary>
        [Comment("Is User Deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date of deletion of the user.
        /// </summary>
        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
