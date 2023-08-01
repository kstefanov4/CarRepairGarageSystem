namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Service"/> entity.
    /// </summary>
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        /// <summary>
        /// Configures the <see cref="Service"/> entity and its relationships with the <see cref="Garage"/> and <see cref="Appointment"/> entities.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            // Configure the one-to-many relationship between Service and Garage entities.
            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure the one-to-many relationship between Service and Appointment entities.
            builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
