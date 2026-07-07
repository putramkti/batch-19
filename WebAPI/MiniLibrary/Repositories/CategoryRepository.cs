using Microsoft.EntityFrameworkCore;
using MiniLibrary.Data;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;

namespace MiniLibrary.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly LibraryDbContext _context;

      public CategoryRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(category => category.Books)
            .OrderBy(category => category.Name)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(category => category.Books)
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<List<Category>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Categories
            .Where(category => ids.Contains(category.Id))
            .ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
