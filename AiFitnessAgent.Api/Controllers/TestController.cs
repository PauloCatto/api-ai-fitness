using AiFitnessAgent.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("users-count")]
    public async Task<IActionResult> GetUsersCount()
    {
        var userCount = await _context.Users.CountAsync();
        return Ok(new { message = "API funcionando!", userCount });
    }
}