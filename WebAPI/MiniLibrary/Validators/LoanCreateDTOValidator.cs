using FluentValidation;
using MiniLibrary.DTOs;

namespace MiniLibrary.Validators;

public class LoanCreateDTOValidator : AbstractValidator<LoanCreateDTO>
{
    public LoanCreateDTOValidator()
    {
        RuleFor(loan => loan.BookId)
            .GreaterThan(0).WithMessage("ID buku harus berupa angka positif.");

        RuleFor(loan => loan.MemberId)
            .GreaterThan(0).WithMessage("ID member harus berupa angka positif.");

        RuleFor(loan => loan.LoanDays)
            .InclusiveBetween(1, 30).WithMessage("Lama peminjaman harus antara 1 sampai 30 hari.");
    }
}
