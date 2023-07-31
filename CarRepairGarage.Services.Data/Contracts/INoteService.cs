namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Note;

    public interface INoteService
    {
        Task CreateNoteAsync(AddNoteViewModel model);
        Task Delete(int id);
        Task DeleteAll(int id);
        Task<bool> Exist(int id);
    }
}
