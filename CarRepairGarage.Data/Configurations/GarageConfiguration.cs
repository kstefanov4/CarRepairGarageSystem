namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Garage"/> entity.
    /// </summary>
    internal class GarageConfiguration : IEntityTypeConfiguration<Garage>
    {
        /// <summary>
        /// Configures the <see cref="Garage"/> entity and its relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Garage> builder)
        {
            // Configure the relationship between Garage and Services.
            // A Garage can have many Services, and each Service belongs to one Garage.
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
