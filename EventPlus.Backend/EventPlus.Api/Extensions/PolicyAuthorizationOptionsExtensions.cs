using System.Reflection;
using EventPlus.Api.Filters;
using EventPlus.Api.Filters.Requirements;
using EventPlus.Core.Attributes;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using NetHub.Shared.Api;

namespace EventPlus.Api.Extensions;

internal static class PolicyAuthorizationOptionsExtensions
{
    private static readonly Type s_permissionType = typeof(CommandPermissions);

    private static CommandPermissionInfoAttribute GetPermissionInfo(this CommandPermissions permission) =>
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
            p =>
            {
                p.RequireClaim(Claims.Permissions,
                    readPermissionName,
                    managePermissionName,
                    CommandPermissionsMetadata.AdminPermission);
                p.UseCommandIdRequirement();
            });

        // MANAGE
        options.AddPolicy(managePolicy,
            p =>
            {
                p.RequireClaim(Claims.Permissions,
                    managePermissionName,
                    CommandPermissionsMetadata.AdminPermission);
                p.UseCommandIdRequirement();
            });
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
            p =>
            {
                p.RequireClaim(Claims.Permissions, readPermissionNames);
                p.UseCommandIdRequirement();
            });

        // MANAGE
        options.AddPolicy(managePolicy,
            p =>
            {
                p.RequireClaim(Claims.Permissions, managePermissionNames);
                p.UseCommandIdRequirement();
            });
    }

    internal static void AddManagePolicy(this AuthorizationOptions options,
        string managePolicy,
        params CommandPermissions[] managePermissions)
    {
        var managePermissionNames = managePermissions
            .Select(p => p.GetPermissionInfo().Key)
            .Append(CommandPermissionsMetadata.AdminPermission).ToArray();

        // MANAGE
        options.AddPolicy(managePolicy,
            p =>
            {
                p.RequireClaim(Claims.Permissions, managePermissionNames);
                p.UseCommandIdRequirement();
            });
    }

    private static void UseCommandIdRequirement(this AuthorizationPolicyBuilder policyBuilder)
    {
        policyBuilder.Requirements.Add(new CommandIdRequirement());
    }
}