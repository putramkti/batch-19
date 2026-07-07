using MiniLibrary.DTOs;

namespace MiniLibrary.Services.Interfaces;

public interface IBookService
{
    Task<ApiResponseDto<List<BookDTO>>> GetAllAsync();
    Task<ApiResponseDto<BookDTO>> GetByIdAsync(int id);
    Task<ApiResponseDto<BookDTO>> CreateAsync(BookCreateDTO createDto);
    Task<ApiResponseDto<BookDTO>> UpdateAsync(int id, BookCreateDTO updateDto);
    Task<ApiResponseDto<string>> DeleteAsync(int id);
}