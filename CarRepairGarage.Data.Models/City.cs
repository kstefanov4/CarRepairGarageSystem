namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    [Comment("City of the garage location")]
    public class City : BaseDeletableModel
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GeneralApplicationConstants.DataValidations.NameMaxLenght)]
        [Comment("City name")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
