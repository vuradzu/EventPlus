using EventPlus.Domain.Entities.Base;
using EventPlus.Domain.Entities.Identity;
using EventPlus.Domain.Enums;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities;

public class Event : ICreatableEntity<long>, ISoftDeletable, IUpdatable
{
    public long Id { get; set; }
    
    public required string Title { get; set; }
    public string? Description { get; set; }
    
    public required Priority Priority {get; set;}
    public required DateTime Date { get; set; }
    
    public long CommandId { get; set; }
    public long CreatorId { get; set; }
    
    public DateTime Created { get; } = DateTime.UtcNow;
    public DateTime? Deleted { get; set; }
    public DateTime? Updated { get; set; }
    public Command? Command { get; set; }
    public AppUser? Creator { get; set; }

    public long? UpdatedBy { get; set; }
    
    public ICollection<Assignment>? Assignments { get; set; }
}