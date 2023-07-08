﻿namespace CarRepairGarage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Microsoft.EntityFrameworkCore;
    
    using CarRepairGarage.Data.Common.Models;

    [Comment("Garage Service")]
    public class GarageService : BaseDeletableModel
    {
        public GarageService()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        [Comment("Garage")]
        public int GarageId { get; set; }

        [ForeignKey(nameof(GarageId))]
        public virtual Garage Garage { get; set; } = null!;

        [Key]
        [Comment("Service")]
        public int ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public virtual Service Service { get; set; } = null!;

        [Comment("Is garage service available")]
        public bool Available { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}