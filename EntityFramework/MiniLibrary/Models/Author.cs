using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.Models;

public class Author
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new List<Book>();
}
