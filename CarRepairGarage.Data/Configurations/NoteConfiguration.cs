namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Configuration class for the <see cref="Note"/> entity.
    /// </summary>
    internal class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        /// <summary>
        /// Configures the <see cref="Note"/> entity and its relationship with the <see cref="Garage"/> entity.
        /// </summary>
        /// <param name="builder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            // Configure the relationship between Note and Garage entities.
            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Note)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
