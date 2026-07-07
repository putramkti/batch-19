using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.DTOs;

public class CategoryCreateDTO
{
    public string Name { get; set; } = string.Empty;
}