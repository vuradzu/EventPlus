using System.Reflection;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using NetHub.Shared.Api;

namespace EventPlus.Core.Extensions;

public static class PolicyAuthorizationOptionsExtensions
{
    private static readonly Type s_permissionType = typeof(CommandPermissions);

    public static CommandPermissionInfoAttribute GetPermissionInfo(this CommandPermissions permission) =>
        s_permissionType
            .GetMember(permission.ToString()).Single()
            .GetCustomAttribute<CommandPermissionInfoAttribute>()!;


    internal static void AddReadManagePolicy(
        this AuthorizationOptions options,
        string readPolicy,
        CommandPermissions readPermission,
        string managePolicy,
        CommandPermissions managePermission)
    {
        var managePermissionName = managePermission.GetPermissionInfo().Key;
        var readPermissionName = readPermission.GetPermissionInfo().Key;

        // READ
        options.AddPolicy(readPolicy,
            p => p.RequireClaim(Claims.Permissions,
                readPermissionName,
                managePermissionName,
                CommandPermissionsMetadata.AdminPermission));

        // MANAGE
        options.AddPolicy(managePolicy,
            p => p.RequireClaim(Claims.Permissions,
                managePermissionName,
                CommandPermissionsMetadata.AdminPermission));
    }

    internal static void AddReadManagePolicy(
        this AuthorizationOptions options,
        string readPolicy,
        IEnumerable<CommandPermissions> readPermissions,
        string managePolicy,
        IEnumerable<CommandPermissions> managePermissions)
    {
        var managePermissionNames = managePermissions
            .Select(p => p.GetPermissionInfo().Key)
            .Append(CommandPermissionsMetadata.AdminPermission).ToArray();
        var readPermissionNames = readPermissions
            .Select(p => p.GetPermissionInfo().Key)
            .Union(managePermissionNames).ToArray();

        // READ
        options.AddPolicy(readPolicy,
            p => p.RequireClaim(Claims.Permissions, readPermissionNames));

        // MANAGE
        options.AddPolicy(managePolicy,
            p => p.RequireClaim(Claims.Permissions, managePermissionNames));
    }
}