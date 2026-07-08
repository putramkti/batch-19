using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniLibrary.DTOs;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly IValidator<MemberCreateDTO> _createValidator;

    public MembersController(IMemberService memberService, IValidator<MemberCreateDTO> createValidator)
    {
        _memberService = memberService;
        _createValidator = createValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        ApiResponseDto<List<MemberDTO>> result = await _memberService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MemberCreateDTO createDto)
    {
        FluentValidation.Results.ValidationResult validationResult = await _createValidator.ValidateAsync(createDto);
        if (!validationResult.IsValid)
        {
            List<string> errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            return BadRequest(ApiResponseDto<MemberDTO>.Failure("Validation failed.",errors));
        }

        ApiResponseDto<MemberDTO> result = await _memberService.CreateAsync(createDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Created(string.Empty, result);
    }
}