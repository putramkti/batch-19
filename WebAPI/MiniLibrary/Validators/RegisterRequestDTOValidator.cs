using FluentValidation;
using MiniLibrary.DTOs;

namespace MiniLibrary.Validators;

public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
{
    public RegisterRequestDTOValidator()
    {
        RuleFor(request => request.FullName)
            .NotEmpty().WithMessage("Nama lengkap wajib diisi.")
            .Length(2, 100).WithMessage("Nama lengkap harus antara 2 sampai 100 karakter.");

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("Email wajib diisi.")
            .EmailAddress().WithMessage("Format email tidak valid.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password wajib diisi.")
            .MinimumLength(6).WithMessage("Password minimal 6 karakter.");
    }
}