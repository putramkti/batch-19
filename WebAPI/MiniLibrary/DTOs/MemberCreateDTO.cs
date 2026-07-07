using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.DTOs;

public class MemberCreateDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public List<int> Loans { get; set; } = new List<int>();
}
