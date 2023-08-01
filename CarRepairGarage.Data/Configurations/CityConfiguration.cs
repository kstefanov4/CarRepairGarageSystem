namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="City"/> entity.
    /// </summary>
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        /// <summary>
        /// Configures the <see cref="City"/> entity and its relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Configure the relationship between City and Addresses.
            // A City can have many Addresses, and each Address belongs to one City.
            builder
                .HasMany(x => x.Addresses)
                .WithOne(x => x.City)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
