using CarRepairGarage.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Data.Configurations
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasOne(a => a.User)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.UserId);

            builder
                .HasOne(a => a.Garage)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.GarageId);

            builder
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId);

            builder
                .HasOne(a => a.Car)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.CarId);


            /*builder
                .HasOne(a => a.GarageService)
                .WithMany(ss => ss.Appointments)
                .HasForeignKey(a => new { a.GarageId, a.ServiceId });*/
        }
    }
}
