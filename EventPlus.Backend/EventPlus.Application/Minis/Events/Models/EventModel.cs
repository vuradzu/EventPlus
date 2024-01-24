using EventPlus.Domain.Enums;

namespace EventPlus.Application.Minis.Events.Models;

public class EventModel
{
    public required long Id { get; init; }
    
    public required string Title { get; init; }
    public string? Description { get; init; }
    
    public long CreatorId { get; set; }
    public long CommandId { get; set; }
    
    public required Priority Priority { get; init; }
    public required DateTime Date { get; init; }

    public DateTime Created { get; init; }
}