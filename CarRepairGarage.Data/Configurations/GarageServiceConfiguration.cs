namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    internal class GarageServiceConfiguration : IEntityTypeConfiguration<GarageService>
    {
        public void Configure(EntityTypeBuilder<GarageService> builder)
        {
            builder
                .HasKey(x => new { x.ServiceId, x.GarageId });

            /*builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.GarageService)
                .OnDelete(DeleteBehavior.NoAction);*/

        }
    }
}
