namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    [Comment("Address for garage")]
    public class Address : BaseDeletableModel
    {
        public Address()
        {
            Garages = new HashSet<Garage>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Address city")]
        [Required]
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; } = null!;

        [Comment("Address street name")]
        [Required]
        [StringLength(GeneralApplicationConstants.DataValidations.NameMaxLenght)]
        public string StreetName { get; set; } = null!;

        [Comment("Address street number")]
        [Required]
        public int StreetNumber { get; set; }

        public virtual ICollection<Garage> Garages { get; set; }
    }
}
