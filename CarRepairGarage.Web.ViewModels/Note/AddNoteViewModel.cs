namespace CarRepairGarage.Web.ViewModels.Note
{
    using System.ComponentModel.DataAnnotations;

    using CarRepairGarage.Common;

    public class AddNoteViewModel
    {
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteTitleMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.NoteTitleMinLenght)]
        public string Title { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteDescriptionMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.NoteDescriptionMaxLenght)]
        public string Description { get; set; } = null!;

        public List<int> GarageIds { get; set; } = new List<int>();
    }
}
