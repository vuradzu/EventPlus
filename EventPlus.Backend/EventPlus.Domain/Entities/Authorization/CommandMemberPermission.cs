using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Authorization;

public class CommandMemberPermission: IEntity<long>
{
    public long Id { get; }
    
    public long MemberId { get; set; }
    public long PermissionId { get; set; }
    
    public CommandMember? Member { get; set; }
    public CommandPermission? Permission { get; set; }
}