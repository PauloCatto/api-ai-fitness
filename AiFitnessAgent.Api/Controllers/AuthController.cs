using AiFitnessAgent.Api.Data;
using AiFitnessAgent.Api.DTOs;
using AiFitnessAgent.Api.Models;
using AiFitnessAgent.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;

    public AuthController(AppDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var normalizedEmail = dto.Email.ToLower().Trim();
        var emailExists = await _context.Users.AnyAsync(u => u.Email == normalizedEmail);
        if (emailExists)
            return BadRequest(new { message = "Este e-mail já está em uso." });

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = normalizedEmail,
            DisplayName = dto.DisplayName,
            PasswordHash = _authService.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _authService.GenerateToken(user);

        return Ok(new
        {
            token,
            user = new 
            { 
                user.Id, 
                user.Email, 
                user.DisplayName, 
                user.OnboardingCompleted,
                user.Age,
                user.Weight,
                user.Goal,
                user.FitnessLevel,
                user.DaysPerWeek
            }
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var normalizedEmail = dto.Email.ToLower().Trim();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == normalizedEmail);
        if (user is null)
            return Unauthorized(new { message = "Este e-mail não está cadastrado em nossa base." });

        var isValidPassword = _authService.VerifyPassword(dto.Password, user.PasswordHash);
        if (!isValidPassword)
            return Unauthorized(new { message = "Senha incorreta. Por favor, tente novamente." });

        var token = _authService.GenerateToken(user);

        return Ok(new
        {
            token,
            user = new 
            { 
                user.Id, 
                user.Email, 
                user.DisplayName, 
                user.OnboardingCompleted,
                user.Age,
                user.Weight,
                user.Goal,
                user.FitnessLevel,
                user.DaysPerWeek
            }
        });
    }
}
