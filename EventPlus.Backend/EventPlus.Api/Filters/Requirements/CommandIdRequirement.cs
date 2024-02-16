using Microsoft.AspNetCore.Authorization;

namespace EventPlus.Api.Filters;

public class CommandIdRequirement : IAuthorizationRequirement
{
    public const string CommandIdHeaderName = "Current-Command-Id-E+";
}