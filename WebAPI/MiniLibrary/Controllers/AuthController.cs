using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.DTOs;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterRequestDTO> _registerValidator;
    private readonly IValidator<LoginRequestDTO> _loginValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegisterRequestDTO> registerValidator,
        IValidator<LoginRequestDTO> loginValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _registerValidator.ValidateAsync(registerDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<string>.Failure("Validation failed.", errors));
        }

        ApiResponseDto<string> result = await _authService.RegisterAsync(registerDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _loginValidator.ValidateAsync(loginDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<AuthResponseDTO>.Failure("Validation failed.", errors));
        }

        ApiResponseDto<AuthResponseDTO> result = await _authService.LoginAsync(loginDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}