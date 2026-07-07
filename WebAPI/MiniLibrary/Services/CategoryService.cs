using AutoMapper;
using MiniLibrary.DTOs;
using MiniLibrary.Models;
using MiniLibrary.Repositories.Interfaces;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<List<CategoryDTO>>> GetAllAsync()
    {
        List<Category> categories = await _categoryRepository.GetAllAsync();
        List<CategoryDTO> dtos = _mapper.Map<List<CategoryDTO>>(categories);
        return ApiResponseDto<List<CategoryDTO>>.Success(dtos);
    }

    public async Task<ApiResponseDto<CategoryDTO>> CreateAsync(CategoryCreateDTO createDto)
    {
        Category newCategory = _mapper.Map<Category>(createDto);

        await _categoryRepository.AddAsync(newCategory);
        await _categoryRepository.SaveChangesAsync();

        CategoryDTO dto = _mapper.Map<CategoryDTO>(newCategory);
        return ApiResponseDto<CategoryDTO>.Success(dto);
    }
}