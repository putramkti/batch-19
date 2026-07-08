using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.DTOs;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly IValidator<LoanCreateDTO> _borrowValidator;

    public LoansController(ILoanService loanService, IValidator<LoanCreateDTO> borrowValidator)
    {
        _loanService = loanService;
        _borrowValidator = borrowValidator;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveLoans()
    {
        ApiResponseDto<List<LoanDTO>> result = await _loanService.GetActiveLoansAsync();
        return Ok(result);
    }

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow([FromBody] LoanCreateDTO createDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _borrowValidator.ValidateAsync(createDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<LoanDTO>.Failure("Validation failed.",errors));
        }

        ApiResponseDto<LoanDTO> result = await _loanService.BorrowAsync(createDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Created(string.Empty, result);
    }

    [HttpPost("{loanId}/return")]
    public async Task<IActionResult> Return(int loanId)
    {
        ApiResponseDto<LoanDTO> result = await _loanService.ReturnAsync(loanId);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}