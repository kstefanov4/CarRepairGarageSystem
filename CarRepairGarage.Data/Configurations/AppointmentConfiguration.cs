namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Appointment"/> entity.
    /// </summary>
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        /// <summary>
        /// Configures the <see cref="Appointment"/> entity and its relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // Configure the relationship between Appointment and User.
            // An Appointment belongs to one User, and each User can have many Appointments.
            builder
                .HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId);

            // Configure the relationship between Appointment and Garage.
            // An Appointment belongs to one Garage, and each Garage can have many Appointments.
            builder
                .HasOne(a => a.Garage)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.GarageId);

            // Configure the relationship between Appointment and Service.
            // An Appointment belongs to one Service, and each Service can have many Appointments.
            builder
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId);

            // Configure the relationship between Appointment and Car.
            // An Appointment belongs to one Car, and each Car can have many Appointments.
            builder
                .HasOne(a => a.Car)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.CarId);

        }
    }
}
