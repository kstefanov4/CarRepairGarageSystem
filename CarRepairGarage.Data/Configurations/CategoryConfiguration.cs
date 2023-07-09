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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasMany(x => x.Services)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Garages)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
