using System.ComponentModel.DataAnnotations;

namespace Mission06_Blake.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Director is required.")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1888, 2100, ErrorMessage = "Year must be between 1888 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        public string Rating { get; set; } // G, PG, PG-13, R

        public bool Edited { get; set; } // ✅ Defaults to false if unchecked

        public string? LentTo { get; set; } // ✅  optional

        [StringLength(25, ErrorMessage = "Notes must be 25 characters or fewer.")]
        public string? Notes { get; set; } // ✅  optional
    }
}