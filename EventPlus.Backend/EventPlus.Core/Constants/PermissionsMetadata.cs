using EventPlus.Core.Attributes;
using EventPlus.Core.Constants;
using EventPlus.Core.Extensions;

namespace NetHub.Shared.Api;

public static class CommandPermissionsMetadata
{
    public static readonly CommandPermissionInfoAttribute[] AllPermissions = Enum.GetValues<CommandPermissions>()
        .Select(p => p.GetPermissionInfo()).ToArray();

    public static readonly string[] Keys = AllPermissions.Select(p => p.Key).ToArray();

    public static readonly string AdminPermission = CommandPermissions.Admin.GetPermissionInfo().Key;
}