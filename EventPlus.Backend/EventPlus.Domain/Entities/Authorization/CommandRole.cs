using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Authorization;

public class CommandRole: ICreatableEntity<long>
{
    public long Id { get; set; }
    public string Title { get; set; }
    
    public DateTime Created { get; } = DateTime.UtcNow;
    
    public ICollection<CommandRolePermission>? RolePermissions { get; set; }
}