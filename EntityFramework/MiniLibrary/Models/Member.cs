namespace MiniLibrary.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Loan> Loans { get; set; } = new List<Loan>();
}
