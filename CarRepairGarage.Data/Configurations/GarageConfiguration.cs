using CarRepairGarage.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

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
