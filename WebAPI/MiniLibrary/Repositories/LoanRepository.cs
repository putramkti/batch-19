using Microsoft.EntityFrameworkCore;
using MiniLibrary.Data;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;

namespace MiniLibrary.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;

    public LoanRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Loan>> GetActiveLoansAsync()
    {
        return await _context.Loans
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .Where(loan => loan.ReturnedAt == null)
            .OrderBy(loan => loan.DueDate)
            .ToListAsync();
    }

    public async Task<Loan?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Loans
            .Include(loan => loan.Book)
            .Include(loan => loan.Member)
            .FirstOrDefaultAsync(loan => loan.Id == id);
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}