namespace MiniLibrary.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public int Stock { get; set; }

    public List<string> CategoryNames { get; set; } = new List<string>();
}
