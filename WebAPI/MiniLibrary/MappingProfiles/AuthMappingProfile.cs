using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;

namespace MiniLibrary.MappingProfiles;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RegisterRequestDTO, ApplicationUser>()
            .ForMember(user => user.UserName, opt => opt.MapFrom(dto => dto.Email));
    }
}