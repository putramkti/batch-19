namespace MiniLibrary.Services;

using Microsoft.EntityFrameworkCore;

using MiniLibrary.Data;
using MiniLibrary.Models;


public class LibraryService
{
    private readonly LibraryDbContext _context;

    public LibraryService(LibraryDbContext context)
    {
        _context = context;
    }

    public Book AddBook(string title, int stock, List<int> authorIds)
    {
        List<Author> matchedAuthors = _context.Authors
            .Where(author => authorIds.Contains(author.Id))
            .ToList();

        Book newBook = new Book
        {
            Title = title,
            Stock = stock,
            Authors = matchedAuthors
        };

        _context.Books.Add(newBook);
        _context.SaveChanges();

        return newBook;
    }

    public Author AddAuthor(string name)
    {
        Author newAuthor = new Author { Name = name };

        _context.Authors.Add(newAuthor);
        _context.SaveChanges();

        return newAuthor;
    }

    public Member AddMember(string name)
    {
        Member newMember = new Member { Name = name };

        _context.Members.Add(newMember);
        _context.SaveChanges();

        return newMember;
    }

    public List<Book> GetAllBooks()
    {
        IQueryable<Book> query = _context.Books
            .Include(b => b.Authors)
            .OrderBy(book => book.Title);

        List<Book> books = query.ToList();

        return books;
    }

    public Book? GetBookById(int bookId)
    {
        Book? book = _context.Books
            .Include(b => b.Authors)
            .FirstOrDefault(b => b.Id == bookId);

        return book;
    }

    public string UpdateBook(int bookId, string title, int stock, List<int> authorIds)
    {
        Book? book = _context.Books
            .Include(b => b.Authors)
            .FirstOrDefault(b => b.Id == bookId);

        if (book is null)
        {
            return "Buku tidak ditemukan.";
        }

        List<Author> matchedAuthors = _context.Authors
            .Where(author => authorIds.Contains(author.Id))
            .ToList();

        if (matchedAuthors.Count == 0)
        {
            return "Tidak ada author valid yang ditemukan.";
        }

        book.Title = title;
        book.Stock = stock;

        book.Authors.Clear();
        foreach (Author author in matchedAuthors)
        {
            book.Authors.Add(author);
        }

        _context.SaveChanges();

        return $"Buku '{book.Title}' berhasil diperbarui.";
    }

    public string DeleteBook(int bookId)
    {
        Book? book = _context.Books
            .Include(b => b.Loans)
            .FirstOrDefault(b => b.Id == bookId);

        if (book is null)
        {
            return "Buku tidak ditemukan.";
        }

        bool hasLoanHistory = book.Loans.Count > 0;
        if (hasLoanHistory)
        {
            return $"'{book.Title}' tidak bisa dihapus karena masih punya riwayat peminjaman.";
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        return $"Buku '{book.Title}' berhasil dihapus.";
    }
    public List<Author> GetAllAuthors()
    {
        List<Author> authors = _context.Authors
            .OrderBy(author => author.Name)
            .ToList();

        return authors;
    }

    public List<Member> GetAllMembers()
    {
        List<Member> members = _context.Members
            .OrderBy(member => member.Name)
            .ToList();

        return members;
    }

    public string BorrowBook(int bookId, int memberId, int loanDays = 7)
    {
        Book? book = _context.Books.Find(bookId);

        if (book == null)
        {
            return "Buku tidak ditemukan.";
        }

        if (book.Stock <= 0)
        {
            return $"Stok '{book.Title}' habis.";
        }

        Member? member = _context.Members.Find(memberId);
        if (member is null)
        {
            return "Member tidak ditemukan.";
        }

        book.Stock = book.Stock - 1;

        Loan newLoan = new Loan
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowedAt = DateTime.Now,
            DueDate = DateTime.Now.AddDays(loanDays)
        };

        _context.Loans.Add(newLoan);
        _context.SaveChanges();

        string dueDateText = newLoan.DueDate.ToString("dd MMM yyyy");
        return $"'{book.Title}' berhasil dipinjam oleh {member.Name}. Jatuh tempo: {dueDateText}.";
    }

    public string ReturnBook(int loanId)
    {
        Loan? loan = _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefault(l => l.Id == loanId);

        if (loan is null)
        {
            return "Data peminjaman tidak ditemukan.";
        }

        if (loan.ReturnedAt is not null)
        {
            return "Buku ini sudah dikembalikan sebelumnya.";
        }

        loan.ReturnedAt = DateTime.Now;
        loan.Book.Stock = loan.Book.Stock + 1;

        _context.SaveChanges();

        return $"'{loan.Book.Title}' dikembalikan oleh {loan.Member.Name}.";
    }

    public List<Loan> GetActiveLoans()
    {
        IQueryable<Loan> query = _context.Loans
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => loan.ReturnedAt == null)
            .OrderBy(loan => loan.DueDate);

        List<Loan> activeLoans = query.ToList();

        return activeLoans;
    }
}



