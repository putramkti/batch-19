using MiniLibrary.Models;

namespace MiniLibrary.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> GetByIdsAsync(List<int> ids);
    Task AddAsync(Category category);
    Task SaveChangesAsync();
}
