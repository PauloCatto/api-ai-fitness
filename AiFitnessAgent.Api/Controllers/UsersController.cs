using System.Security.Claims;
using System.Text.Json;
using AiFitnessAgent.Api.Data;
using AiFitnessAgent.Api.DTOs;
using AiFitnessAgent.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("onboarding")]
    [Authorize]
    public async Task<IActionResult> SaveOnboarding([FromBody] OnboardingDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Token inválido." });

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        if (user is null)
            return NotFound(new { message = "Usuário não encontrado." });

        user.DisplayName = dto.DisplayName;
        user.Age = dto.Age;
        user.Weight = dto.Weight;
        user.Goal = dto.Goal;
        user.FitnessLevel = dto.FitnessLevel;
        user.Limitations = JsonSerializer.Serialize(dto.Limitations);
        user.Injuries = dto.Injuries;
        user.DaysPerWeek = dto.DaysPerWeek;
        user.OnboardingCompleted = true;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            user.Id,
            user.Email,
            user.DisplayName,
            user.OnboardingCompleted,
            user.Age,
            user.Weight,
            user.Goal,
            user.FitnessLevel,
            user.DaysPerWeek,
            Limitations = dto.Limitations
        });
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] OnboardingDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Token inválido." });

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        if (user is null)
            return NotFound(new { message = "Usuário não encontrado." });

        user.DisplayName = dto.DisplayName;
        user.Age = dto.Age;
        user.Weight = dto.Weight;
        user.Goal = dto.Goal;
        user.FitnessLevel = dto.FitnessLevel;
        user.Limitations = JsonSerializer.Serialize(dto.Limitations);
        user.Injuries = dto.Injuries;
        user.DaysPerWeek = dto.DaysPerWeek;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            user.Id,
            user.Email,
            user.DisplayName,
            user.OnboardingCompleted,
            user.Age,
            user.Weight,
            user.Goal,
            user.FitnessLevel,
            user.DaysPerWeek,
            Limitations = dto.Limitations
        });
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue("sub");

        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Token inválido." });

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        if (user is null)
            return NotFound(new { message = "Usuário não encontrado." });

        return Ok(new
        {
            user.Id,
            user.Email,
            user.DisplayName,
            user.OnboardingCompleted,
            user.Age,
            user.Weight,
            user.Goal,
            user.FitnessLevel,
            user.DaysPerWeek,
            Limitations = string.IsNullOrEmpty(user.Limitations) 
                ? new List<string>() 
                : JsonSerializer.Deserialize<List<string>>(user.Limitations)
        });
    }
}
