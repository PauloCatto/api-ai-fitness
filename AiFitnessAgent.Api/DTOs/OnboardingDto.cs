namespace AiFitnessAgent.Api.DTOs;

public class OnboardingDto
{
    public string DisplayName { get; set; } = string.Empty;
    public int Age { get; set; }
    public double Weight { get; set; }
    public string Goal { get; set; } = string.Empty;
    public string FitnessLevel { get; set; } = string.Empty;
    public List<string> Limitations { get; set; } = [];
    public string Injuries { get; set; } = string.Empty;
    public int DaysPerWeek { get; set; }
}
