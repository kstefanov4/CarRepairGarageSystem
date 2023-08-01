namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents a service that can be provided by a repair garage.
    /// </summary>
    [Comment("Garage service")]
    public class Service : BaseDeletableModel
    {
        public Service()
        {
            Garages = new HashSet<GarageService>();
            Appointments = new HashSet<Appointment>();
        }

        /// <summary>
        /// Gets or sets the ID of the service.
        /// </summary>
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        [Required]
        [MaxLength(GeneralApplicationConstants.Validations.ServiceNameMaxLenght)]
        [Comment("Service name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the service.
        /// </summary>
        [MaxLength(250)]
        [Comment("Service Description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of garage services associated with this service.
        /// </summary>
        public virtual ICollection<GarageService> Garages { get; set; }

        /// <summary>
        /// Gets or sets the collection of appointments associated with this service.
        /// </summary>
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
