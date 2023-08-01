namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Car"/> entity.
    /// </summary>
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        /// <summary>
        /// Configures the <see cref="Car"/> entity and its relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            // Configure the relationship between Car and Appointments.
            // A Car can have many Appointments, and each Appointment belongs to one Car.
            builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Car)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
