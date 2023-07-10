using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Services.Data.Garage.Contracts;
using CarRepairGarage.Web.ViewModels;
using CarRepairGarage.Web.ViewModels.Garage;
using CarRepairGarage.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace CarRepairGarage.Services.Data.Garage
{
    public class GarageService : IGarageService
    {
        private readonly IRepository repo;
        public GarageService(IRepository repository)
        {
            repo = repository;
        }

        public async Task<IEnumerable<GarageViewModel>> GetAllGaragesAsync()
        {
            var garages = await repo
                            .AllReadonly<CarRepairGarage.Data.Models.Garage>()
                            .Where(x => x.IsDeleted == false)
                            .Include(x => x.Services)
                            .OrderByDescending(x => x.Id)
                            .Take(3)
                            .Select(x => new GarageViewModel()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Category = x.Category.Name,
                                City = x.Address.City.Name,
                                Services = x.Services.Select(x => x.Service.Name).ToList(),
                                ImageUrl = x.ImageUrl
                            }).ToListAsync();
            return garages;
        }
    }
}
