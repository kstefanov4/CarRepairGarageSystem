namespace CarRepairGarage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Common.Models;

    [Comment("User Appointments")]
    public class Appointment : BaseDeletableModel
    {
        public Appointment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Comment("Primary key")]
        [Key]
        public string Id { get; set; } = null!;

        [Comment("Date of the appointment")]
        public DateTime DateTime { get; set; }

        [Required]
        [Comment("User appointed")]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Appointed garage")]
        public string GarageId { get; set; } = null!;

        [ForeignKey(nameof(GarageId))]
        public virtual Garage Garage { get; set; } = null!;

        [Required]
        [Comment("Appointed service")]
        public int ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; } = null!;

        public virtual GarageService GarageService { get; set; } = null!;

        [Comment("Is appointment confirmed")]
        public bool? Confirmed { get; set; }

    }
}
