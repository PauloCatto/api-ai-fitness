namespace AiFitnessAgent.Api.DTOs;

public class SaveWorkoutPlanDto
{
    public string PlanData { get; set; } = string.Empty;
}

public class WorkoutPlanResponseDto
{
    public Guid Id { get; set; }
    public string PlanData { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
}
