using System;
using System.Collections.Generic;

namespace AiFitnessAgent.Api.DTOs;

public class SaveWorkoutSessionDto
{
    public Guid Id { get; set; }
    public Guid PlanId { get; set; }
    public int DayIndex { get; set; }
    public string? Feedback { get; set; }
    public List<string> CompletedExerciseIds { get; set; } = new();
    public int DurationMinutes { get; set; }
}
