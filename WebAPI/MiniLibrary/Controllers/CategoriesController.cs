using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.DTOs;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CategoryCreateDTO> _createValidator;

    public CategoriesController(ICategoryService categoryService, IValidator<CategoryCreateDTO> createValidator)
    {
        _categoryService = categoryService;
        _createValidator = createValidator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        ApiResponseDto<List<CategoryDTO>> result = await _categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDTO createDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _createValidator.ValidateAsync(createDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<CategoryDTO>.Failure(errors));
        }

        ApiResponseDto<CategoryDTO> result = await _categoryService.CreateAsync(createDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Created(string.Empty, result);
    }
}