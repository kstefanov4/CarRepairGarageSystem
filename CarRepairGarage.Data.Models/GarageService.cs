namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a service offered by a repair garage.
    /// </summary>
    [Comment("Garage Service")]
    public class GarageService
    {
        public GarageService()
        {
            //Appointments = new HashSet<Appointment>();
        }

        /// <summary>
        /// Gets or sets the ID of the garage offering the service.
        /// </summary>
        [Key]
        [Comment("Garage")]
        public int GarageId { get; set; }

        /// <summary>
        /// Gets or sets the garage offering the service.
        /// </summary>
        [ForeignKey(nameof(GarageId))]
        public virtual Garage Garage { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ID of the service being offered.
        /// </summary>
        [Key]
        [Comment("Service")]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the service being offered.
        /// </summary>
        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the service is available at the garage.
        /// </summary>
        /*[Comment("Is garage service available")]
        public bool Available { get; set; }*/
    }
}
