using CarRepairGarage.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

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
