namespace CarRepairGarage.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    [Comment("Extended Identity User")]
    public class ApplicationUser : IdentityUser<string>, IDeletableEntity
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            Roles = new HashSet<IdentityUserRole<string>>();
            Appointments = new HashSet<Appointment>();
            Garages = new HashSet<Garage>();
            Cars = new HashSet<Car>();
        }

        [Comment("Is User Deleted")]
        public bool IsDeleted { get; set; }

        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
