namespace MiniLibrary.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<BookDTO> Books { get; set; } = new List<BookDTO>();
}
