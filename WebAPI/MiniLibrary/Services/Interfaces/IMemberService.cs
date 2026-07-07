using MiniLibrary.DTOs;

namespace MiniLibrary.Services.Interfaces;

public interface IMemberService
{
    Task<ApiResponseDto<List<MemberDTO>>> GetAllAsync();
    Task<ApiResponseDto<MemberDTO>> CreateAsync(MemberCreateDTO createDto);
}