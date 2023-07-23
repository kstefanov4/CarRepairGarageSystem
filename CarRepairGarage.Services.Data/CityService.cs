using CarRepairGarage.Data.Models;
using CarRepairGarage.Data.Repositories.Contracts;
using CarRepairGarage.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository _repository;

        public CityService(IRepository repository)
        {
            this._repository = repository;
        }
        public async Task<IEnumerable<string>> AllCitiesNameAsync()
        {
            return await _repository.AllReadonly<City>()
                .Where(x => x.IsDeleted == false)
                .Select(x => x.Name)
                .ToListAsync();
        }
    }
}
