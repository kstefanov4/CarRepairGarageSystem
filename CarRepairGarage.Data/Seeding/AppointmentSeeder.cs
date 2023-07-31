namespace CarRepairGarage.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    public class AppointmentSeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Appointments.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Guid userGuid = userManager.FindByNameAsync(CarRepairGarage.Common.GeneralApplicationConstants.AccountsData.UserEmail).Result.Id;

            var garages = await dbContext.Garages.ToListAsync();

            var appointments = new List<Appointment>();

            var carsIds = await dbContext.Cars
                    .Where(x => x.UserId == userGuid).Select(x => x.Id)
                    .ToListAsync();

            foreach (var garage in garages)
            {
                var service = await dbContext.GaragesServices
                    .Where(x => x.GarageId == garage.Id)
                    .FirstOrDefaultAsync();

                appointments.Add(new Appointment
                {
                    Date = DateTime.Now.AddDays(15),
                    Time = new TimeSpan(10, 0, 0),
                    GarageId = garage.Id,
                    ServiceId = service!.ServiceId,
                    UserId = userGuid,
                    CarId = 1
                });
            }


            await dbContext.Appointments.AddRangeAsync(appointments.ToArray());
            await dbContext.SaveChangesAsync();
        }
    }
}
