using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Authorization;

namespace EventPlus.Domain.Extensions;

public static class CommandMemberExtensions
{
    public static ICollection<CommandPermission> GetMemberPermissions(this CommandMember member)
    {
        var rolesPermissions = member
            .CommandMemberRoles?
            .SelectMany(r => r.Role?.RolePermissions)
            .Select(rp => rp.Permission) ?? [];

        var memberPermissions = member
            .CommandMemberPermissions?
            .Select(mp => mp.Permission) ?? [];

        return rolesPermissions.Intersect(memberPermissions).ToArray()!;
    }

    public static bool HasPermission(this CommandMember member, string permissionTitle)
    {
        var permissions = member.GetMemberPermissions();

        return permissions.FirstOrDefault(p => p.Title == permissionTitle) is not null;
    }

    public static bool HasPermission(this CommandMember member, long permissionId)
    {
        var permissions = member.GetMemberPermissions();
        
        return permissions.FirstOrDefault(p => p.Id == permissionId) is not null;
    }
}