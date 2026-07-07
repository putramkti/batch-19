using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.DTOs;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IValidator<BookCreateDTO> _bookValidator;

    public BooksController(IBookService bookService, IValidator<BookCreateDTO> bookValidator)
    {
        _bookService = bookService;
        _bookValidator = bookValidator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        ApiResponseDto<List<BookDTO>> result = await _bookService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        ApiResponseDto<BookDTO> result = await _bookService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookCreateDTO createDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _bookValidator.ValidateAsync(createDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<BookDTO>.Failure(errors));
        }

        ApiResponseDto<BookDTO> result = await _bookService.CreateAsync(createDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Created(string.Empty, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookCreateDTO updateDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _bookValidator.ValidateAsync(updateDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<BookDTO>.Failure(errors));
        }

        ApiResponseDto<BookDTO> result = await _bookService.UpdateAsync(id, updateDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        ApiResponseDto<string> result = await _bookService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}