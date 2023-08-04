namespace CarRepairGarage.Services.DIConfiguration
{
    using Microsoft.Extensions.DependencyInjection;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data.Repositories;
    using CarRepairGarage.Data.Seeding;
    using CarRepairGarage.Services.Contracts;

    /// <summary>
    /// A static class that contains extension methods for registering application services with the DI container.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers the application services and their corresponding implementations with the DI container.
        /// </summary>
        /// <param name="services">The service collection to which the services will be added.</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Add a scoped instance of the Seeder class for seeding the database.
            services.AddScoped<Seeder>();

            // Add a scoped instance of the Repository class as the implementation of the IRepository interface.
            services.AddScoped<IRepository, Repository>();

            // Add a scoped instance of the Services.GarageService class as the implementation of the IGarageService interface.
            services.AddScoped<IGarageService, Services.GarageService>();

            // Add a scoped instance of the CategoryService class as the implementation of the ICategoryService interface.
            services.AddScoped<ICategoryService, CategoryService>();

            // Add a scoped instance of the ServiceService class as the implementation of the IServiceService interface.
            services.AddScoped<IServiceService, ServiceService>();

            // Add a scoped instance of the CarService class as the implementation of the ICarService interface.
            services.AddScoped<ICarService, CarService>();

            // Add a scoped instance of the AppointmentService class as the implementation of the IAppointmentService interface.
            services.AddScoped<IAppointmentService, AppointmentService>();

            // Add a scoped instance of the CityService class as the implementation of the ICityService interface.
            services.AddScoped<ICityService, CityService>();

            // Add a scoped instance of the NoteService class as the implementation of the INoteService interface.
            services.AddScoped<INoteService, NoteService>();

            // Add a scoped instance of the UserService class as the implementation of the IUserService interface.
            services.AddScoped<IUserService, UserService>();
        }
    }


}
