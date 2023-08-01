namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="GarageService"/> entity.
    /// </summary>
    internal class GarageServiceConfiguration : IEntityTypeConfiguration<GarageService>
    {
        /// <summary>
        /// Configures the <see cref="GarageService"/> entity and its primary key.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<GarageService> builder)
        {
            // Configure the composite primary key for the GarageService entity.
            builder
                .HasKey(x => new { x.ServiceId, x.GarageId });
        }
    }
}
