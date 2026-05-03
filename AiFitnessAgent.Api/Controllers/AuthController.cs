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

    // POST /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (emailExists)
            return BadRequest(new { message = "Este e-mail já está em uso." });

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            DisplayName = dto.DisplayName,
            PasswordHash = _authService.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _authService.GenerateToken(user);

        return Ok(new
        {
            token,
            user = new { user.Id, user.Email, user.DisplayName }
        });
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null)
            return Unauthorized(new { message = "Credenciais inválidas." });

        var isValidPassword = _authService.VerifyPassword(dto.Password, user.PasswordHash);
        if (!isValidPassword)
            return Unauthorized(new { message = "Credenciais inválidas." });

        var token = _authService.GenerateToken(user);

        return Ok(new
        {
            token,
            user = new { user.Id, user.Email, user.DisplayName }
        });
    }
}
