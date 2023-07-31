namespace CarRepairGarage.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using CarRepairGarage.Data.Common.Models;
    using CarRepairGarage.Common;

    [Comment("Garage Notes")]
    public class Note : BaseDeletableModel
    {
        public Note()
        {
            Garages = new HashSet<Garage>();
        }
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteTitleMaxLenght)]
        [Comment("Note Title")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GeneralApplicationConstants.Validations.NoteDescriptionMaxLenght)]
        [Comment("Note description")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Note Image")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Comment("Is Note Vissible")]
        public bool Vissible { get; set; }

        public virtual ICollection<Garage> Garages { get; set; }

    }
}
