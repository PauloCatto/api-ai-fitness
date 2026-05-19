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
            Goals = new[]
            {
                new { Value = "hypertrophy", Label = "Hipertrofia", Icon = "💪", Description = "Foco em ganho de massa muscular" },
                new { Value = "strength", Label = "Força", Icon = "🏋️", Description = "Aumento de força e potência" },
                new { Value = "weight_loss", Label = "Emagrecimento", Icon = "🔥", Description = "Queima de gordura e definição" },
                new { Value = "endurance", Label = "Resistência", Icon = "🏃", Description = "Condicionamento cardiovascular e estamina" }
            },
            Levels = new[]
            {
                new { Value = "beginner", Label = "Iniciante", Icon = "🌱", Description = "Começando agora ou retornando após muito tempo" },
                new { Value = "intermediate", Label = "Intermediário", Icon = "⚡", Description = "Treina consistentemente há alguns meses" },
                new { Value = "advanced", Label = "Avançado", Icon = "🔥", Description = "Treina seriamente e de forma estruturada há anos" }
            },
            Splits = new[]
            {
                new { Value = "ai_choice", Label = "Escolha da IA", Icon = "🤖", Description = "Deixe a IA analisar seu perfil e escolher a melhor divisão" },
                new { Value = "full_body", Label = "Corpo Inteiro (Full Body)", Icon = "🤸", Description = "Treina o corpo todo a cada sessão" },
                new { Value = "push_pull_legs", Label = "Empurrar/Puxar/Pernas (PPL)", Icon = "⚖️", Description = "Divide por padrão de movimento" },
                new { Value = "upper_lower", Label = "Superior/Inferior (Upper/Lower)", Icon = "⬆️", Description = "Alterna entre membros superiores e inferiores" },
                new { Value = "bro_split", Label = "ABCDE (Músculo por dia)", Icon = "💪", Description = "Foca intensamente em um grupo muscular por dia" }
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
