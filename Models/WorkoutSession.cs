using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiFitnessAgent.Api.Models;

public class WorkoutSession
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid PlanId { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int DayIndex { get; set; }

    public string? Feedback { get; set; }

    [Required]
    public string CompletedExerciseIdsJson { get; set; } = "[]";

    public int DurationMinutes { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [ForeignKey("PlanId")]
    public WorkoutPlan? Plan { get; set; }

    [NotMapped]
    public List<string> CompletedExerciseIds 
    { 
        get => System.Text.Json.JsonSerializer.Deserialize<List<string>>(CompletedExerciseIdsJson) ?? new List<string>();
        set => CompletedExerciseIdsJson = System.Text.Json.JsonSerializer.Serialize(value);
    }
}
