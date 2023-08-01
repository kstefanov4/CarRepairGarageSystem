namespace CarRepairGarage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents an appointment for a user with a garage for a specific service and car.
    /// </summary>
    [Comment("User Appointments")]
    public class Appointment
    {
        public Appointment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the primary key of the appointment.
        /// </summary>
        [Comment("Primary key")]
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Gets or sets the date of the appointment.
        /// </summary>
        [Comment("Date of the appointment")]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the time of the appointment.
        /// </summary>
        [Comment("Time of the appointment")]
        [Column(TypeName = "Time")]
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who made the appointment.
        /// </summary>
        [Required]
        [Comment("User appointed")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who made the appointment.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the garage where the appointment is made.
        /// </summary>
        [Required]
        [Comment("Appointed garage")]
        public int GarageId { get; set; }

        /// <summary>
        /// Gets or sets the garage where the appointment is made.
        /// </summary>
        [ForeignKey(nameof(GarageId))]
        public virtual Garage Garage { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the service for which the appointment is made.
        /// </summary>
        [Required]
        [Comment("Appointed service")]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the service for which the appointment is made.
        /// </summary>
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the car for which the appointment is made.
        /// </summary>
        [Required]
        [Comment("Appointed car")]
        public int CarId { get; set; }

        /// <summary>
        /// Gets or sets the car for which the appointment is made.
        /// </summary>
        [ForeignKey(nameof(CarId))]
        public virtual Car Car { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the appointment is confirmed.
        /// </summary>
        [Comment("Is appointment confirmed")]
        public bool? Confirmed { get; set; }

    }
}
