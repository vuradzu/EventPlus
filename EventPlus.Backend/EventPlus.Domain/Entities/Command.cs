using EventPlus.Domain.Entities.Base;
using EventPlus.Domain.Entities.Identity;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities;

public class Command : ICreatableEntity<long>, ISoftDeletable, IUpdatable
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Avatar { get; set; }

    public long CreatorId { get; set; }
    public long? UpdatedBy { get; set; }

    public DateTime Created { get; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
    
    
    public ICollection<InviteCode>? InviteCodes { get; set; }
    public ICollection<CommandMember>? CommandMembers { get; set; }
    public ICollection<Event>? Events { get; set; }
    
    public AppUser? Creator { get; set; }
}