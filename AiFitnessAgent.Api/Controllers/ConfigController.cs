using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiFitnessAgent.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("gemini-key")]
    public IActionResult GetGeminiKey()
    {
        var apiKey = _configuration["Gemini:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            return NotFound("Gemini API Key not configured.");
        }

        return Ok(new { apiKey });
    }
}
