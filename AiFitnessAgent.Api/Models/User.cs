namespace AiFitnessAgent.Api.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

    // Onboarding fields
    public bool OnboardingCompleted { get; set; } = false;
    public int? Age { get; set; }
    public double? Weight { get; set; }
    public string? Goal { get; set; }
    public string? FitnessLevel { get; set; }
    public string? Limitations { get; set; } // JSON array as string
    public string? Injuries { get; set; }
    public int? DaysPerWeek { get; set; }
    public string? WorkoutSplit { get; set; } // ex: Full Body, ABC, Push/Pull/Legs
    public string? FocusAreas { get; set; } // JSON array of muscle groups
    public int? CardioMinutes { get; set; }
}
