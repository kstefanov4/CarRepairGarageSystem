using CarRepairGarage.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Data.Configurations
{
    internal class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            builder
                .HasMany(x => x.Services)
                .WithOne(x => x.Garage)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Garage)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
