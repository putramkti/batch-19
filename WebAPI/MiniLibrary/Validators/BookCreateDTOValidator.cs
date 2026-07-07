using FluentValidation;
using MiniLibrary.DTOs;

namespace MiniLibrary.Validators;

public class BookCreateDTOValidator : AbstractValidator<BookCreateDTO>
{
    public BookCreateDTOValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Judul buku wajib diisi.")
            .Length(2, 200).WithMessage("Judul buku harus antara 2 sampai 200 karakter.");

        RuleFor(book => book.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stok tidak boleh bernilai negatif.");

        RuleFor(book => book.CategoryIds)
            .NotEmpty().WithMessage("Buku harus memiliki minimal satu kategori.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Terdapat ID kategori yang duplikat.");
    }
}
