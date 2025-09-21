using System.ComponentModel.DataAnnotations;

namespace BookManager.WebAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MinLength(2, ErrorMessage = "Title must be at least 3 characters long")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [MinLength(2, ErrorMessage = "Author must be at least 2 characters long")]
        public string Author { get; set; } = string.Empty;
    }
}
