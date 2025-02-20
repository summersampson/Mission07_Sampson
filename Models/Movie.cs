using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission07_Sampson.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Year is required.")]
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later.")]
        public int Year { get; set; }

        public string? Director { get; set; }
        public string? Rating { get; set; }

        [Required(ErrorMessage = "Edited is required.")]
        public bool Edited { get; set; }

        [Required(ErrorMessage = "Copied to Plex is required.")]
        public bool CopiedToPlex { get; set; }

        [Required(ErrorMessage = "LentTo is required.")]
        public string LentTo { get; set; } = "";

        [MaxLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
        public string? Notes { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
