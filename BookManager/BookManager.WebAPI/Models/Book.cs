using System.ComponentModel.DataAnnotations;

namespace BookManager.WebAPI.Models
{
    public class Book
    {
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        public string Title { get; set; } = string.Empty;
    }
}
