namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Common;
    using CarRepairGarage.Data.Common.Models;

    [Comment("Repair Garage")]
    public class Garage : BaseDeletableModel
    {
        public Garage()
        {
            Appointments = new HashSet<Appointment>();
            Services = new HashSet<GarageService>();
        }

        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GeneralApplicationConstants.DataValidations.NameMaxLenght)]
        [Comment("Garage name")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("Garage Image")]
        public string ImageUrl { get; set; } = null!;
        
        [Comment("Garage Owner")]
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? Owner { get; set; }

        [Comment("Garage Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        [Required]
        [Comment("Garage address")]
        public int AddressId { get; set; }
        
        public virtual Address Address { get; set; } = null!;

        public virtual ICollection<GarageService> Services { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
