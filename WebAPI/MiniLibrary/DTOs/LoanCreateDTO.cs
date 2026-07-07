using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.DTOs;

public class LoanCreateDTO
{
    public int BookId { get; set; }

    public int MemberId { get; set; }
    public int LoanDays { get; set; } = 7;

}
