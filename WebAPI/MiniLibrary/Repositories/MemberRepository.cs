using Microsoft.EntityFrameworkCore;
using MiniLibrary.Data;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;

namespace MiniLibrary.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Member>> GetAllAsync()
    {
        return await _context.Members
            .OrderBy(member => member.Name)
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members.FindAsync(id);
    }

    public async Task<Member?> GetByIdWithLoansAsync(int id)
    {
        return await _context.Members
            .Include(member => member.Loans)
            .ThenInclude(loan => loan.Book)
            .FirstOrDefaultAsync(member => member.Id == id);
    }

    public async Task AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}