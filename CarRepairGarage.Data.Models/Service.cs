namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    [Comment("Garage service")]
    public class Service : BaseDeletableModel
    {
        public Service()
        {
            Garages = new HashSet<GarageService>();
            Appointments = new HashSet<Appointment>();
        }
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GeneralApplicationConstants.DataValidations.NameMinLenght)]
        [Comment("Service name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(GeneralApplicationConstants.DataValidations.DescriptionMaxLength)]
        [Comment("Service description")]
        public string Description { get; set; } = null!;
        
        [Comment("Service Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<GarageService> Garages { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
