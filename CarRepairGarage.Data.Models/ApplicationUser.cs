namespace CarRepairGarage.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// This is custome user class that works with the defoult ASP.NET Core Identity.
    /// You can add additional info to the build-in users.
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

        [Comment("Is User Deleted")]
        public bool IsDeleted { get; set; }

        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
