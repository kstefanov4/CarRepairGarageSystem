using CarRepairGarage.Data.Models;
using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Services.Contracts;
using CarRepairGarage.Web.ViewModels.Garage;
using CarRepairGarage.Web.ViewModels.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services
{
    public class ServiceService : IServiceService
    {
        protected readonly IRepository repository;

        public ServiceService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<ServiceViewModel>> GetAllServiceAsync()
        {
            var services = await repository.AllReadonly<Service>()
                                    .Where(x => x.IsDeleted == false)
                                    .Include(x => x.Garages)
                                    .Select(x => new ServiceViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Garages = x.Garages.Select(g => new GarageViewModel
                                        {
                                            Id = g.Garage.Id,
                                            Name = g.Garage.Name
                                        }).ToList()
                                    }).ToListAsync();
            return services;
        }
    }
}
