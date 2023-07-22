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
        [MaxLength(GeneralApplicationConstants.DataValidations.NameMaxLenght)]
        [Comment("Service name")]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        [Comment("Service Description")]
        public string? Description { get; set; }

        public virtual ICollection<GarageService> Garages { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
