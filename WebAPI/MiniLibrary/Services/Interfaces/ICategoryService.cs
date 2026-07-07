using MiniLibrary.DTOs;

namespace MiniLibrary.Services.Interfaces;

public interface ICategoryService
{
    Task<ApiResponseDto<List<CategoryDTO>>> GetAllAsync();
    Task<ApiResponseDto<CategoryDTO>> CreateAsync(CategoryCreateDTO createDto);
}