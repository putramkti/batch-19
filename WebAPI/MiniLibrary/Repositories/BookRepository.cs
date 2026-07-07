using Microsoft.EntityFrameworkCore;
using MiniLibrary.Data;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;

namespace MiniLibrary.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(book => book.Categories)
            .OrderBy(book => book.Title)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(book => book.Categories)
            .FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<Book?> GetByIdWithLoansAsync(int id)
    {
        return await _context.Books
            .Include(book => book.Loans)
            .FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public void Remove(Book book)
    {
        _context.Books.Remove(book);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
