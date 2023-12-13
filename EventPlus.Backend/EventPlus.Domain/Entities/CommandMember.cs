using EventPlus.Domain.Entities.Identity;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities;

public class CommandMember : IEntity<long>
{
    public long Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }

    public long CommandId { get; set; }
    public required long AppUserId { get; set; }

    public Command? Command { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime MemberSince { get; set; } = DateTime.UtcNow;
}