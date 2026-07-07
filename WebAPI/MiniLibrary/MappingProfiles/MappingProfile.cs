using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;

namespace MiniLibrary.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>()
           .ForMember(dto => dto.BookTitles, opt => opt.MapFrom(category => category.Books.Select(book => book.Title)));

        CreateMap<Book, BookDTO>()
            .ForMember(dto => dto.CategoryNames, opt => opt.MapFrom(book => book.Categories.Select(category => category.Name)));

        CreateMap<Member, MemberDTO>();

        CreateMap<Loan, LoanDTO>()
            .ForMember(dto => dto.BookTitle, opt => opt.MapFrom(loan => loan.Book.Title))
            .ForMember(dto => dto.MemberName, opt => opt.MapFrom(loan => loan.Member.Name))
            .ForMember(dto => dto.IsOverdue, opt => opt.MapFrom(loan => loan.ReturnedAt == null && loan.DueDate < DateTime.Now));

        CreateMap<CategoryCreateDTO, Category>();

        CreateMap<BookCreateDTO, Book>()
           .ForMember(book => book.Categories, opt => opt.Ignore());

        CreateMap<MemberCreateDTO, Member>();

        CreateMap<LoanCreateDTO, Loan>()
            .ForMember(loan => loan.BorrowedAt, opt => opt.Ignore())
            .ForMember(loan => loan.DueDate, opt => opt.Ignore())
            .ForMember(loan => loan.Book, opt => opt.Ignore())
            .ForMember(loan => loan.Member, opt => opt.Ignore());

        CreateMap<RegisterRequestDTO, ApplicationUser>()
            .ForMember(user => user.UserName, opt => opt.MapFrom(dto => dto.Email));
    }
}
