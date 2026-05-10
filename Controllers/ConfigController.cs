using Microsoft.AspNetCore.Mvc;

namespace AiFitnessAgent.Api.Controllers;

[ApiController]
[Route("api/config")]
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
        var key = _configuration["Gemini:ApiKey"];
        if (!string.IsNullOrEmpty(key) && key.Length > 8)
        {
            Console.WriteLine($"[Backend] Servindo chave que começa com: {key.Substring(0, 8)}...");
        }
        else
        {
            Console.WriteLine("[Backend] Alerta: Chave da API está vazia ou muito curta!");
        }
        return Ok(new { ApiKey = key });
    }

    [HttpGet("workout-options")]
    public IActionResult GetWorkoutOptions()
    {
        return Ok(new
        {
            Splits = new[]
            {
                new { Value = "ai_choice", Label = "Escolha da IA" },
                new { Value = "full_body", Label = "Corpo Inteiro (Full Body)" },
                new { Value = "push_pull_legs", Label = "Empurrar/Puxar/Pernas (PPL)" },
                new { Value = "upper_lower", Label = "Superior/Inferior (Upper/Lower)" },
                new { Value = "bro_split", Label = "ABCDE (Músculo por dia)" }
            },
            MuscleGroups = new[]
            {
                new { Value = "chest", Label = "Peito" },
                new { Value = "back", Label = "Costas" },
                new { Value = "shoulders", Label = "Ombros" },
                new { Value = "biceps", Label = "Bíceps" },
                new { Value = "triceps", Label = "Tríceps" },
                new { Value = "legs", Label = "Pernas Completas" },
                new { Value = "glutes", Label = "Glúteos" },
                new { Value = "core", Label = "Core / Abdômen" }
            }
        });
    }
}
