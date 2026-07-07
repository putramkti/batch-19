using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.DTOs;

public class BookCreateDTO
{
    public string Title { get; set; } = string.Empty;
    
    public int Stock { get; set; }

    public List<int> CategoryIds { get; set; } = new List<int>();
}
