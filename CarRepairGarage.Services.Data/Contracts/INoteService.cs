namespace CarRepairGarage.Services.Contracts
{
    using CarRepairGarage.Web.ViewModels.Note;

    /// <summary>
    /// This interface defines the contract for a service that handles operations related to notes in the car repair garage application.
    /// </summary>
    public interface INoteService
    {
        /// <summary>
        /// Creates a new note in the database based on the provided add note view model.
        /// </summary>
        /// <param name="model">The add note view model containing the details of the new note.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task CreateNoteAsync(AddNoteViewModel model);

        /// <summary>
        /// Deletes the note with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the note to delete.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task Delete(int id);

        /// <summary>
        /// Deletes all notes associated with the specified garage ID from the database.
        /// </summary>
        /// <param name="id">The ID of the garage whose notes will be deleted.</param>
        /// <returns>An asynchronous task that represents the operation.</returns>
        Task DeleteAll(int id);

        /// <summary>
        /// Checks if a note with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the note to check for existence.</param>
        /// <returns>An asynchronous task that represents the operation and returns true if the note exists, otherwise false.</returns>
        Task<bool> Exist(int id);
    }

}
