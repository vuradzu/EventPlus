using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Authorization;

public class CommandRolePermission: IEntity<long>
{
    public long Id { get; set; }
    
    public long RoleId { get; set; }
    public long PermissionId { get; set; }
    
    public CommandRole? Role { get; set; }
    public CommandPermission? Permission { get; set; }
}