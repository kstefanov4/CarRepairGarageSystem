namespace CarRepairGarage.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Configurations;
    using CarRepairGarage.Data.Models;
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<GarageService>()
                .HasKey(x => new { x.ServiceId, x.GarageId });

            builder.Entity<Address>()
                .HasMany(x => x.Garages)
                .WithOne(x => x.Address)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>()
                .HasMany(x => x.Services)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>()
                .HasMany(x => x.Garages)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<City>()
                .HasMany(x => x.Addresses)
                .WithOne(x => x.City)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Garage>()
                .HasMany(x => x.Services)
                .WithOne(x => x.Garage)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Garage>()
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Garage)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<GarageService>()
                .HasMany(x => x.Appointments)
                .WithOne(x => x.GarageService)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Service>()
                .HasMany(x => x.Garages)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Service>()
                .HasMany(x => x.Appointments)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.NoAction);

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