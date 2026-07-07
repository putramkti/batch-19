using MiniLibrary.DTOs;

namespace MiniLibrary.Services.Interfaces;

public interface ILoanService
{
    Task<ApiResponseDto<List<LoanDTO>>> GetActiveLoansAsync();
    Task<ApiResponseDto<LoanDTO>> BorrowAsync(LoanCreateDTO createDto);
    Task<ApiResponseDto<LoanDTO>> ReturnAsync(int loanId);
}