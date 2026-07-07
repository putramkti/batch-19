namespace MiniLibrary.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<string> BookTitles { get; set; } = new List<string>();
}
