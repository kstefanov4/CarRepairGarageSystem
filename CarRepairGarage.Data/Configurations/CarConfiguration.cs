namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CarRepairGarage.Data.Models;

    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Car)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
