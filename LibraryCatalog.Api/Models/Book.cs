using System.ComponentModel.DataAnnotations;

namespace LibraryCatalog.Api.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Title field is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Author field is required.")]
    public string Author { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;

    [Range(1800, 2026, ErrorMessage = "YearPublished must be between 1800 and 2026.")]
    public int YearPublished { get; set; }
}