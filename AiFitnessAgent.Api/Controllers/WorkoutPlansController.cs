using System.Security.Claims;
using AiFitnessAgent.Api.Data;
using AiFitnessAgent.Api.DTOs;
using AiFitnessAgent.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/workoutplans")]
[Authorize]
public class WorkoutPlansController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkoutPlansController(AppDbContext context)
    {
        _context = context;
    }

    private Guid GetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(raw!);
    }

    [HttpGet("ping")]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { message = "WorkoutPlans Controller is ALIVE!" });

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest()
    {
        var userId = GetUserId();
        var plan = await _context.WorkoutPlans
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.GeneratedAt)
            .Select(p => new WorkoutPlanResponseDto
            {
                Id = p.Id,
                PlanData = p.PlanData,
                GeneratedAt = p.GeneratedAt
            })
            .FirstOrDefaultAsync();

        // Retornamos Ok(null) em vez de NotFound para não disparar erro no console do navegador
        if (plan == null) return Ok(null);

        return Ok(plan);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SaveWorkoutPlanDto dto)
    {
        var userId = GetUserId();
        
        var plan = new WorkoutPlan
        {
            UserId = userId,
            PlanData = dto.PlanData,
            GeneratedAt = DateTime.UtcNow
        };

        _context.WorkoutPlans.Add(plan);
        await _context.SaveChangesAsync();

        return Ok(new WorkoutPlanResponseDto
        {
            Id = plan.Id,
            PlanData = plan.PlanData,
            GeneratedAt = plan.GeneratedAt
        });
    }
}
