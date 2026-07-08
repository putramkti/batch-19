using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public LoanService(
        ILoanRepository loanRepository,
        IBookRepository bookRepository,
        IMemberRepository memberRepository,
        IMapper mapper)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<List<LoanDTO>>> GetActiveLoansAsync()
    {
        List<Loan> loans = await _loanRepository.GetActiveLoansAsync();
        List<LoanDTO> dtos = _mapper.Map<List<LoanDTO>>(loans);
        return ApiResponseDto<List<LoanDTO>>.Success(dtos, "Berhasil mengambil data peminjaman aktif.");
    }

    public async Task<ApiResponseDto<LoanDTO>> BorrowAsync(LoanCreateDTO createDto)
    {
        Book? book = await _bookRepository.GetByIdAsync(createDto.BookId);
        if (book is null)
        {
            return ApiResponseDto<LoanDTO>.Failure("Buku tidak ditemukan.");
        }

        if (book.Stock <= 0)
        {
            return ApiResponseDto<LoanDTO>.Failure($"Stok '{book.Title}' habis.");
        }

        Member? member = await _memberRepository.GetByIdAsync(createDto.MemberId);
        if (member is null)
        {
            return ApiResponseDto<LoanDTO>.Failure("Member tidak ditemukan.");
        }

        book.Stock = book.Stock - 1;

        Loan newLoan = new Loan
        {
            BookId = createDto.BookId,
            MemberId = createDto.MemberId,
            BorrowedAt = DateTime.Now,
            DueDate = DateTime.Now.AddDays(createDto.LoanDays)
        };

        await _loanRepository.AddAsync(newLoan);
        await _loanRepository.SaveChangesAsync();

        newLoan.Book = book;
        newLoan.Member = member;

        LoanDTO dto = _mapper.Map<LoanDTO>(newLoan);
        return ApiResponseDto<LoanDTO>.Success(dto, "Buku berhasil dipinjam.");
    }

    public async Task<ApiResponseDto<LoanDTO>> ReturnAsync(int loanId)
    {
        Loan? loan = await _loanRepository.GetByIdWithDetailsAsync(loanId);
        if (loan is null)
        {
            return ApiResponseDto<LoanDTO>.Failure("Data peminjaman tidak ditemukan.");
        }

        if (loan.ReturnedAt is not null)
        {
            return ApiResponseDto<LoanDTO>.Failure("Buku ini sudah dikembalikan sebelumnya.");
        }

        loan.ReturnedAt = DateTime.Now;
        loan.Book.Stock = loan.Book.Stock + 1;

        await _loanRepository.SaveChangesAsync();

        LoanDTO dto = _mapper.Map<LoanDTO>(loan);
        return ApiResponseDto<LoanDTO>.Success(dto, "Buku berhasil dikembalikan.");
    }
}