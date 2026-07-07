using FluentValidation;
using MiniLibrary.DTOs;

namespace MiniLibrary.Validators;

public class LoginRequestDTOValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestDTOValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty().WithMessage("Email wajib diisi.")
            .EmailAddress().WithMessage("Format email tidak valid.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password wajib diisi.");
    }
}