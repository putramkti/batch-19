using FluentValidation;
using MiniLibrary.DTOs;

namespace MiniLibrary.Validators;

public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
{
    public CategoryCreateDTOValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("Nama kategori wajib diisi.")
            .Length(2, 100).WithMessage("Nama kategori harus antara 2 sampai 100 karakter.");
    }
}
