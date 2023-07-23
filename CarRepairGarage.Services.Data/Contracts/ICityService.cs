using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services.Contracts
{
    public interface ICityService
    {
        Task<IEnumerable<string>> AllCitiesNameAsync();
    }
}
