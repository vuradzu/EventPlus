using EventPlus.Api.Filters;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using NetHub.Shared.Api.Constants;

namespace EventPlus.Api.Extensions;

public static class PolicyAuthorizationExtensions
{
    public static void AddPoliciesAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, CommandIdRequirementHandler>();
        services.AddAuthorization(options =>
        {
            // /commands
            options.AddManagePolicy(
                managePolicy: Policies.HasManageCommandPermission,
                managePermissions: CommandPermissions.ManageCommand);

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