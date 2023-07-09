namespace CarRepairGarage.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using CarRepairGarage.Data.Models;
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Address)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
