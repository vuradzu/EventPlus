using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Authorization;

public class CommandMemberRole: IEntity<long>
{
    public long Id { get; }
    
    public long MemberId { get; set; }
    public long RoleId { get; set; }
    
    public CommandMember? Member { get; set; }
    public CommandRole? Role { get; set; }
}