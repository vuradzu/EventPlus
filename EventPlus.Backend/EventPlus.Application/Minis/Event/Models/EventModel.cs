using EventPlus.Domain.Entities;

namespace EventPlus.Application.Minis.Commands.Models;

public class EventModel
{
    public required long Id { get; init; }
    
    public required string Title { get; init; }
    public string? Description { get; init; }
    
    public required Priority Priority {get; init;}
    public required DateTime DateTime { get; init; }

    public DateTime Created { get; init; }
}