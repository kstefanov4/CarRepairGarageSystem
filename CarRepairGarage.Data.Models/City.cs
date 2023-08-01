namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    /// <summary>
    /// Represents a city where a garage is located in the database.
    /// </summary>
    [Comment("City of the garage location")]
    public class City : BaseDeletableModel
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        /// <summary>
        /// Gets or sets the primary key of the city.
        /// </summary>
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        [Required]
        [MaxLength(GeneralApplicationConstants.Validations.AddressStreetNameMaxLenght)]
        [Comment("City name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of addresses in this city.
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
