using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;

namespace MiniLibrary.MappingProfiles;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDTO>()
            .ForMember(dto => dto.CategoryNames, opt => opt.MapFrom(book => book.Categories.Select(category => category.Name)));

        CreateMap<BookCreateDTO, Book>()
            .ForMember(book => book.Categories, opt => opt.Ignore());
    }
}