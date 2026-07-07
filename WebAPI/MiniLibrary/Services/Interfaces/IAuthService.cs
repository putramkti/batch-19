using MiniLibrary.DTOs;

namespace MiniLibrary.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponseDto<string>> RegisterAsync(RegisterRequestDTO registerDto);
    Task<ApiResponseDto<AuthResponseDTO>> LoginAsync(LoginRequestDTO loginDto);
}