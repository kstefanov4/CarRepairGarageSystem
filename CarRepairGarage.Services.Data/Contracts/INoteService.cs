using CarRepairGarage.Web.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairGarage.Services.Contracts
{
    public interface INoteService
    {
        Task CreateNoteAsync(AddNoteViewModel model);
        Task Delete(int id);
        Task DeleteAll(int id);
        Task<bool> Exist(int id);
    }
}
