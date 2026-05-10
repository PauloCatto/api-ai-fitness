namespace AiFitnessAgent.Api.Models;

public class WorkoutPlan
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string PlanData { get; set; } = string.Empty; // JSON structure from frontend
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public User? User { get; set; }
}
