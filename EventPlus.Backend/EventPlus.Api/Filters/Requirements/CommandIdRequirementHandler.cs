using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using NeerCore.Exceptions;

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
            var message = "No Command-E+ header provided";
            context.Fail(new AuthorizationFailureReason(this, message));
            throw new ForbidException(message);
        }

        if (string.IsNullOrWhiteSpace(commandIdFromAccessToken))
        {
            var message = "Access token has no required command Id";
            context.Fail(new AuthorizationFailureReason(this, message));
            throw new ForbidException(message);
        }

        if (commandIdFromHeader != commandIdFromAccessToken)
        {
            var message = "The access token is from the wrong chat";
            context.Fail(new AuthorizationFailureReason(this, message));
            throw new ForbidException(message);
        }

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}