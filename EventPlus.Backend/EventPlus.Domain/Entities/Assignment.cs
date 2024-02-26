using EventPlus.Domain.Entities.Base;
using EventPlus.Domain.Entities.Identity;
using EventPlus.Domain.Enums;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities;

public class Assignment : ICreatableEntity<long>, ISoftDeletable, IUpdatable
{
    public long Id { get; set; }
    
    public required string Title { get; set; }
    public required string? Description { get; set; }
    
    public required DateTime? Date { get; set; }
    public required Priority Priority {get; set;}
    
    public bool CanBeCompleted { get; set; }
    public required bool  Completed  { get; set; }
    
    public long AssigneeId { get; set; }
    public long CreatorId { get; set; }
    public long EventId { get; set; }

    public DateTime? CompletionTime  { get; set; }
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Deleted { get; set; }
    public DateTime? Updated { get; set; }
        
    public AppUser? Assignee { get; set; }
    public AppUser? Creator { get; set; }
    public Event? Event { get; set; }

    public long? UpdatedBy { get; set; }
}