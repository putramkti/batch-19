using MiniLibrary.Models;

namespace MiniLibrary.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<List<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member?> GetByIdWithLoansAsync(int id);
    Task AddAsync(Member member);
    Task SaveChangesAsync();
}
