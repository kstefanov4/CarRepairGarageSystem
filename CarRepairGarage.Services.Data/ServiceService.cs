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
        protected readonly IRepository _repository;

        public ServiceService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> AllServicesNameAsync()
        {
            return await _repository.AllReadonly<Service>()
                .Select(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<ServiceViewModel>> GetAllServiceAsync()
        {
            var services = await _repository.AllReadonly<Service>()
                                    .Where(x => x.IsDeleted == false)
                                    .Include(x => x.Garages)
                                    .Select(x => new ServiceViewModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Description = x.Description,
                                        Garages = x.Garages.Select(g => new GarageViewModel
                                        {
                                            Id = g.Garage.Id,
                                            Name = g.Garage.Name
                                        }).ToList()
                                    }).ToListAsync();
            return services;
        }

        public async Task<string> GetServiceByIdAsync(int id)
        {
            return await _repository.AllReadonly<Service>()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Select(x => x.Name)
                .FirstAsync();
        }
    }
}
