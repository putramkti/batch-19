using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;

namespace MiniLibrary.MappingProfiles;

public class LoanMappingProfile : Profile
{
    public LoanMappingProfile()
    {
        CreateMap<Loan, LoanDTO>()
            .ForMember(dto => dto.BookTitle, opt => opt.MapFrom(loan => loan.Book.Title))
            .ForMember(dto => dto.MemberName, opt => opt.MapFrom(loan => loan.Member.Name))
            .ForMember(dto => dto.IsOverdue, opt => opt.MapFrom(loan => loan.ReturnedAt == null && loan.DueDate < DateTime.Now));
    }
}