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
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
