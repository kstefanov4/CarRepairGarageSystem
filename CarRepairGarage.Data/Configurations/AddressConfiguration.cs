namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Address"/> entity.
    /// </summary>
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        /// <summary>
        /// Configures the <see cref="Address"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // Configure the relationship between Address and Garages.
            // An Address can have many Garages, and each Garage has one Address.
            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Address)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
