using EventPlus.Domain.Enums;

namespace EventPlus.Application.Minis.Assignments.Models;

public class AssignmentModel
{
    public required long Id { get; init; }
    public required string Title { get; set; }
    public required string? Description { get; set; }

    public required Priority Priority { get; set; }
    public DateTime? Date { get; set; }

    public required bool Completed { get; set; }
    public required bool CanBeCompleted { get; set; }

    public long AssigneeId { get; set; }
    public long CreatorId { get; set; }
    public long EventId { get; set; }

    public DateTime? CompletionTime { get; }
    public DateTime Created { get; }
}