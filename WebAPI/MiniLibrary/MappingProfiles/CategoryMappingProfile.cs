using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;

namespace MiniLibrary.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDTO>()
            .ForMember(dto => dto.BookTitles, opt => opt.MapFrom(category => category.Books.Select(book => book.Title)));

        CreateMap<CategoryCreateDTO, Category>();
    }
}