namespace CarRepairGarage.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Configurations;
    using CarRepairGarage.Data.Models;

    /// <summary>
    /// Represents the database context for the CarRepairGarage application.
    /// This class inherits from IdentityDbContext to provide user and role management functionality.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Overrides the OnModelCreating method to apply entity configurations using EntityTypeBuilder.
        /// Applies custom configurations for various entities to customize how they are mapped to the database tables.
        /// </summary>
        /// <param name="builder">The model builder used to configure the entities.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new GarageConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new GarageServiceConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());

            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Gets or sets the entity set for addresses in the database.
        /// </summary>
        public DbSet<Address> Addresses { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for cities in the database.
        /// </summary>
        public DbSet<City> Cities { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for appointments in the database.
        /// </summary>
        public DbSet<Appointment> Appointments { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for cars in the database.
        /// </summary>
        public DbSet<Car> Cars { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for categories in the database.
        /// </summary>
        public DbSet<Category> Categories { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for garages in the database.
        /// </summary>
        public DbSet<Garage> Garages { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for garage services in the database.
        /// </summary>
        public DbSet<GarageService> GaragesServices { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for notes in the database.
        /// </summary>
        public DbSet<Note> Notes { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity set for services in the database.
        /// </summary>
        public DbSet<Service> Services { get; set; } = null!;
    }
}