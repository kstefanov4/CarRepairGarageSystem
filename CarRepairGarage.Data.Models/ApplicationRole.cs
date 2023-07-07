namespace CarRepairGarage.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    [Comment("Extended Identity Role")]
    public class ApplicationRole : IdentityRole<string>, IDeletableEntity
    {
        /*public ApplicationRole()
            : this(null)
        {
        }*/

        public ApplicationRole(string name)
            : base(name)
        {
            Id = Guid.NewGuid().ToString();
        }
        [Comment("Is Application role deleted")]
        public bool IsDeleted { get; set; }

        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }
    }
}
