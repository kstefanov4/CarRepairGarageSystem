namespace CarRepairGarage.Web.ViewModels.Note
{
    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    using CarRepairGarage.Common;

    /// <summary>
    /// Represents a view model for adding a new note.
    /// </summary>
    public class AddNoteViewModel
    {
        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteTitleMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.NoteTitleMinLenght)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the image of the note.
        /// </summary>
        [Required]
        public IFormFile? Image { get; set; }

        /// <summary>
        /// Gets or sets the description of the note.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteDescriptionMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.NoteDescriptionMinLenght)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of garage IDs associated with the note.
        /// </summary>
        public List<int> GarageIds { get; set; } = new List<int>();
    }

}
