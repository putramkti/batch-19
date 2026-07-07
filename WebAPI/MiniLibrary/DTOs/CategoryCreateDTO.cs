using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.DTOs;

public class CategoryCreateDTO
{
    public string Name { get; set; } = string.Empty;

    public List<int> BookIds { get; set; } = new List<int>();
}