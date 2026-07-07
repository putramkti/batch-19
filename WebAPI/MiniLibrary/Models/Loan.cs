namespace MiniLibrary.Models;

public class Loan
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }

    public DateTime? ReturnedAt { get; set; }
}
