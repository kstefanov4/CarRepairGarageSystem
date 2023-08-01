namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents a user's car in the database.
    /// </summary>
    [Comment("User Car")]
    public class Car : BaseDeletableModel
    {
        public Car()
        {
            Appointments = new HashSet<Appointment>();
        }

        /// <summary>
        /// Gets or sets the primary key of the car.
        /// </summary>
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the VIN (Vehicle Identification Number) of the car.
        /// </summary>
        [Required]
        [Comment("Car VIN number")]
        public string VIN { get; set; } = null!;

        /// <summary>
        /// Gets or sets the make of the car.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarMakeMaxLenght)]
        [Comment("Car Make")]
        public string Make { get; set; } = null!;

        /// <summary>
        /// Gets or sets the model of the car.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarMakeMaxLenght)]
        [Comment("Primary Model")]
        public string Model { get; set; } = null!;

        /// <summary>
        /// Gets or sets the year of production of the car.
        /// </summary>
        [Required]
        [Comment("Car year of production")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who owns the car.
        /// </summary>
        [Required]
        [Comment("User of the car")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who owns the car.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of appointments associated with the car.
        /// </summary>
        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
