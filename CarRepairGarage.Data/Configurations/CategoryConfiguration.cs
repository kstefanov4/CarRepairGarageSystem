namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Category"/> entity.
    /// </summary>
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Configures the <see cref="Category"/> entity and its relationships.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configure the relationship between Category and Garages.
            // A Category can have many Garages, and each Garage belongs to one Category.
            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
