namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents an address for a garage.
    /// </summary>
    [Comment("Address for garage")]
    public class Address : BaseDeletableModel
    {
        public Address()
        {
            Garages = new HashSet<Garage>();
        }

        /// <summary>
        /// Gets or sets the primary key of the address.
        /// </summary>
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the city associated with this address.
        /// </summary>
        [Comment("Address city")]
        [Required]
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the city associated with this address.
        /// </summary>
        [ForeignKey(nameof(CityId))]
        public City City { get; set; } = null!;

        /// <summary>
        /// Gets or sets the street name of the address.
        /// </summary>
        [Comment("Address street name")]
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.AddressStreetNameMaxLenght)]
        public string StreetName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the street number of the address.
        /// </summary>
        [Comment("Address street number")]
        [Required]
        public int StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of garages associated with this address.
        /// </summary>
        public virtual ICollection<Garage> Garages { get; set; }
    }
}
