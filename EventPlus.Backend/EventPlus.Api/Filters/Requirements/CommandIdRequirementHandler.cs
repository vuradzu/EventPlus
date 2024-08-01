using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace EventPlus.Api.Filters.Requirements;

public class CommandIdRequirementHandler(IHttpContextAccessor httpContextAccessor, IUserProvider userProvider)
    : AuthorizationHandler<CommandIdRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CommandIdRequirement requirement)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            return Task.CompletedTask;
        }

        var httpContext = httpContextAccessor.HttpContext;
        var commandIdHeaderExists =
            httpContext!.Request.Headers.TryGetValue(HeaderConstants.CommandIdHeaderName,
                out var commandIdFromHeader);
        var commandIdFromAccessToken = userProvider.GetClaimValue(Claims.CommandId);

        if (!commandIdHeaderExists)
        {
            context.Fail(new AuthorizationFailureReason(this, "No Command-E+ header provided"));
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(commandIdFromAccessToken))
        {
            context.Fail(new AuthorizationFailureReason(this, "Access token has no required command Id"));
            return Task.CompletedTask;
        }

        if (commandIdFromHeader != commandIdFromAccessToken)
        {
            context.Fail(new AuthorizationFailureReason(this, "The access token is from the wrong chat"));
            return Task.CompletedTask;
        }

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}