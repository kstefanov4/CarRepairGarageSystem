namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Common.Models;
    using CarRepairGarage.Common;

    /// <summary>
    /// Represents a note associated with a repair garage.
    /// </summary>
    [Comment("Garage Notes")]
    public class Note : BaseDeletableModel
    {
        public Note()
        {
            Garages = new HashSet<Garage>();
        }

        /// <summary>
        /// Gets or sets the ID of the note.
        /// </summary>
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteTitleMaxLenght)]
        [Comment("Note Title")]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the note.
        /// </summary>
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteDescriptionMaxLenght)]
        [Comment("Note description")]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the URL of the image associated with the note.
        /// </summary>
        [Required]
        [Comment("Note Image")]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the note is visible.
        /// </summary>
        [Required]
        [Comment("Is Note Vissible")]
        public bool Vissible { get; set; }

        /// <summary>
        /// Gets or sets the collection of garages associated with the note.
        /// </summary>
        public virtual ICollection<Garage> Garages { get; set; }

    }
}
