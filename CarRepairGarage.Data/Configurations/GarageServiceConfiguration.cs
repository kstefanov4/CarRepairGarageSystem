namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    public class GarageServiceConfiguration : IEntityTypeConfiguration<GarageService>
    {
        public void Configure(EntityTypeBuilder<GarageService> builder)
        {
            builder.HasKey(x => new { x.GarageId, x.ServiceId });

            builder
                .HasOne(gs => gs.Service)
                .WithMany()
                .HasForeignKey(gs => gs.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
