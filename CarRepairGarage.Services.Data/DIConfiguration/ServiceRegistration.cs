
namespace CarRepairGarage.Services.DIConfiguration
{
    using Microsoft.Extensions.DependencyInjection;
    
    using CarRepairGarage.Data.Repositories.Contracts;
    using CarRepairGarage.Data.Repositories;
    using CarRepairGarage.Data.Seeding;
    using CarRepairGarage.Services.Contracts;
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<Seeder>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IGarageService, Services.GarageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<INoteService, NoteService>();
        }
    }

}
