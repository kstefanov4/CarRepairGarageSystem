namespace CarRepairGarage.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Configurations;
    using CarRepairGarage.Data.Models;
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new GarageConfiguration());
            builder.ApplyConfiguration(new GarageServiceConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Garage> Garages { get; set; } = null!;
        public DbSet<GarageService> GaragesServices { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
    }
}