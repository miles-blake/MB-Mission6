using System.ComponentModel.DataAnnotations;

namespace Mission06_Blake.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        // Foreign key for Category. EF will infer the FK from "CategoryId" + the nav property "Category"
        [Required(ErrorMessage = "A Category must be selected")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }

        // Director is optional
        public string? Director { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1888, 9999, ErrorMessage = "Year cannot be before 1888")]
        public int Year { get; set; }

        // Rating is optional
        public string? Rating { get; set; }

        [Required(ErrorMessage = "Please specify if this is Edited or not")]
        public bool Edited { get; set; }

        [Required(ErrorMessage = "Please specify if this is Copied to Plex")]
        public bool CopiedToPlex { get; set; }

        public string? LentTo { get; set; }

        [MaxLength(25)]
        public string? Notes { get; set; }
    }
}