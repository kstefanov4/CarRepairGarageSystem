namespace CarRepairGarage.Web.ViewModels.Note
{
    using System.ComponentModel.DataAnnotations;

    using CarRepairGarage.Common;
    using Microsoft.AspNetCore.Http;

    public class AddNoteViewModel
    {
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteTitleMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.NoteTitleMinLenght)]
        public string Title { get; set; } = null!;

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteDescriptionMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.NoteDescriptionMinLenght)]
        public string Description { get; set; } = null!;

        public List<int> GarageIds { get; set; } = new List<int>();
    }
}
