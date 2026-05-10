using System.Security.Claims;
using AiFitnessAgent.Api.Data;
using AiFitnessAgent.Api.DTOs;
using AiFitnessAgent.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/sessions")]
[Authorize]
public class SessionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SessionsController(AppDbContext context)
    {
        _context = context;
    }

    private Guid GetUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        return Guid.Parse(raw!);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SaveWorkoutSessionDto dto)
    {
        var userId = GetUserId();

        var session = await _context.WorkoutSessions
            .FirstOrDefaultAsync(s => s.Id == dto.Id);

        if (session == null)
        {
            session = new WorkoutSession
            {
                Id = dto.Id,
                UserId = userId,
                PlanId = dto.PlanId,
                Date = DateTime.UtcNow,
                DayIndex = dto.DayIndex,
                CompletedExerciseIds = dto.CompletedExerciseIds,
                DurationMinutes = dto.DurationMinutes,
                Feedback = dto.Feedback
            };
            _context.WorkoutSessions.Add(session);
        }
        else
        {
            session.CompletedExerciseIds = dto.CompletedExerciseIds;
            session.DurationMinutes = dto.DurationMinutes;
            session.Feedback = dto.Feedback;
            session.DayIndex = dto.DayIndex;
        }

        await _context.SaveChangesAsync();
        return Ok(session);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var sessions = await _context.WorkoutSessions
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
        return Ok(sessions);
    }
}
