using CarRepairGarage.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Data.Seeding
{
    public class AppointmentSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Appointments.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Guid userGuid = userManager.FindByNameAsync("user@mail.com").Result.Id;

            var garages = await dbContext.Garages.ToListAsync();

            var appointments = new List<Appointment>();

            foreach (var garage in garages)
            {
                var service = await dbContext.GaragesServices.Where(x => x.GarageId == garage.Id).FirstOrDefaultAsync();

                appointments.Add(new Appointment
                {
                    DateTime = DateTime.Now.AddDays(15),
                    GarageId = garage.Id,
                    ServiceId = service.ServiceId,
                    UserId = userGuid,
                    IsDeleted = false
                });
            }


            await dbContext.Appointments.AddRangeAsync(appointments.ToArray());
            await dbContext.SaveChangesAsync();
        }
    }
}
