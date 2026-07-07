using System.ComponentModel.DataAnnotations;

namespace MiniLibrary.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Stock { get; set; }

    public List<Category> Categories { get; set; } = new List<Category>();

    public List<Loan> Loans { get; set; } = new List<Loan>();
}
