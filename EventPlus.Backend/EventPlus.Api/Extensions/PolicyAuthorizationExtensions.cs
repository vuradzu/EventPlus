using EventPlus.Api.Filters;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace EventPlus.Api.Extensions;

public static class PolicyAuthorizationExtensions
{
    public static void AddPoliciesAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, CommandIdRequirementHandler>();
        services.AddAuthorization(options =>
        {
            // /commands
            options.AddReadManagePolicy(
                readPolicy: Policies.HasCommandMemberPermission,
                readPermissions: [CommandPermissions.CommandMember],
                managePolicy: Policies.HasManageCommandPermission,
                managePermissions: [CommandPermissions.ManageCommand]);

            options.AddManagePolicy(
                managePolicy: Policies.HasManageCommandMembersPermission,
                managePermissions: CommandPermissions.ManageCommandMembers);

            // /events
            options.AddManagePolicy(
                managePolicy: Policies.HasManageEventPermission,
                managePermissions: CommandPermissions.ManageEvent);
        });
    }
}