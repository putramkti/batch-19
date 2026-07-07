namespace MiniLibrary.DTOs;

public class LoanDTO
{
    public int Id { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public bool IsOverdue { get; set; }
}
