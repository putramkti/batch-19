using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniLibrary.DTOs;
using MiniLibrary.Models;
using MiniLibrary.Services.Interfaces;

namespace MiniLibrary.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<string>> RegisterAsync(RegisterRequestDTO registerDto)
    {
        ApplicationUser? existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser is not null)
        {
            return ApiResponseDto<string>.Failure("Email sudah terdaftar.");
        }

        ApplicationUser newUser = _mapper.Map<ApplicationUser>(registerDto);

        IdentityResult result = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!result.Succeeded)
        {
            List<string> errors = result.Errors.Select(error => error.Description).ToList();
            return ApiResponseDto<string>.Failure(errors);
        }

        return ApiResponseDto<string>.Success("Registrasi berhasil.");
    }

    public async Task<ApiResponseDto<AuthResponseDTO>> LoginAsync(LoginRequestDTO loginDto)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
        {
            return ApiResponseDto<AuthResponseDTO>.Failure("Email atau password salah.");
        }

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
        {
            return ApiResponseDto<AuthResponseDTO>.Failure("Email atau password salah.");
        }

        AuthResponseDTO responseDto = GenerateJwtToken(user);
        return ApiResponseDto<AuthResponseDTO>.Success(responseDto);
    }

    private AuthResponseDTO GenerateJwtToken(ApplicationUser user)
    {
        string jwtKey = _configuration["Jwt:Key"] ?? string.Empty;
        string jwtIssuer = _configuration["Jwt:Issuer"] ?? string.Empty;
        string jwtAudience = _configuration["Jwt:Audience"] ?? string.Empty;
        int expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60");

        DateTime expiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes);

        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        SigningCredentials signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: signingCredentials);

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDTO
        {
            Token = tokenString,
            ExpiresAt = expiresAt,
            FullName = user.FullName
        };
    }
}