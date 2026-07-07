using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.DTOs;

public class MemberDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // public List<LoanDTO> Loans { get; set; } = new List<LoanDTO>();
}
