namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents a repair garage in the database.
    /// </summary>
    [Comment("Repair Garage")]
    public class Garage : BaseDeletableModel
    {
        public Garage()
        {
            Appointments = new HashSet<Appointment>();
            Services = new HashSet<GarageService>();
        }

        /// <summary>
        /// Gets or sets the primary key of the garage.
        /// </summary>
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the garage.
        /// </summary>
        [Required]
        [MaxLength(GeneralApplicationConstants.Validations.GarageNameMaxLenght)]
        [Comment("Garage name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the garage's image.
        /// </summary>
        [Required]
        [Comment("Garage Image")]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the owner (user) of the garage.
        /// </summary>
        [Comment("Garage Owner")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who owns the garage.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser Owner { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category the garage belongs to.
        /// </summary>
        [Comment("Garage Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category the garage belongs to.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the address where the garage is located.
        /// </summary>
        [Required]
        [Comment("Garage address")]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address where the garage is located.
        /// </summary>
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the note associated with the garage (optional).
        /// </summary>
        [Comment("Garage Note")]
        public int? NoteId { get; set; }

        /// <summary>
        /// Gets or sets the note associated with the garage (optional).
        /// </summary>
        [ForeignKey(nameof(NoteId))]
        public virtual Note? Note { get; set; }

        /// <summary>
        /// Gets or sets the collection of services provided by the garage.
        /// </summary>
        public virtual ICollection<GarageService> Services { get; set; }

        /// <summary>
        /// Gets or sets the collection of appointments scheduled at the garage.
        /// </summary>
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
