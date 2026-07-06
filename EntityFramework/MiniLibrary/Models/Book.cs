namespace MiniLibrary.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Stock { get; set; }

    public List<Author> Authors { get; set; } = new List<Author>();

    public List<Loan> Loans { get; set; } = new List<Loan>();
}
