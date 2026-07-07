using MiniLibrary.Models;

namespace MiniLibrary.Repositories.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> GetByIdWithLoansAsync(int id);
    Task AddAsync(Book book);
    void Remove(Book book);
    Task SaveChangesAsync();
}
