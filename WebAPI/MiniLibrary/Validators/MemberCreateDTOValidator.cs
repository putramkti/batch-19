using FluentValidation;
using MiniLibrary.DTOs;

namespace MiniLibrary.Validators;

public class MemberCreateDTOValidator :AbstractValidator<MemberCreateDTO>
{
     public MemberCreateDTOValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty().WithMessage("Nama member wajib diisi.")
            .Length(2, 100).WithMessage("Nama member harus antara 2 sampai 100 karakter.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Nama member hanya boleh berisi huruf dan spasi.");
    }
}
