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
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Comment("Time of the appointment")]
        [Column(TypeName = "Time")]
        public TimeSpan Time { get; set; }

        [Required]
        [Comment("User appointed")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [Comment("Appointed garage")]
        public int GarageId { get; set; } 

        [ForeignKey(nameof(GarageId))]
        public virtual Garage Garage { get; set; } = null!;

        [Required]
        [Comment("Appointed service")]
        public int ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; } = null!;

        [Comment("Is appointment confirmed")]
        public bool? Confirmed { get; set; }

    }
}
