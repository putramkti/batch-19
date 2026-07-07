using MiniLibrary.Models;

namespace MiniLibrary.Repositories.Interfaces;

public interface ILoanRepository
{
    Task<List<Loan>> GetActiveLoansAsync();
    Task<Loan?> GetByIdWithDetailsAsync(int id);
    Task AddAsync(Loan loan);
    Task SaveChangesAsync();
}