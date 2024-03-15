using System.Security.Claims;
using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using EventPlus.Domain.Entities.Identity;
using EventPlus.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;

namespace EventPlus.Infrastructure.Services;

[Service]
public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _accessor;
    private AppUser? _userProfile;

    private UserManager<AppUser> UserManager =>
        _accessor.HttpContext!.RequestServices.GetRequiredService<UserManager<AppUser>>();

    public UserProvider(IHttpContextAccessor accessor) => _accessor = accessor;

    private ClaimsPrincipal User => _accessor.HttpContext!.User;

    public long UserId => User.GetUserId();
    public string UserName => User.GetUsername();

    public long? TryGetUserId()
    {
        var claimResult = User.TryGetClaimWithoutAuthorization(Claims.Id, out var claim);
        if (!claimResult)
            return null;

        var longResult = long.TryParse(claim?.Value, out long userId);

        return longResult ? userId : null;
    }

    public async Task<AppUser> GetUserAsync()
    {
        try
        {
            var user = await UserManager.FindByIdAsync(UserId.ToString());
            if (user is null)
                throw new UnauthorizedException("Authorized user required");

            return _userProfile ??= user;
        }
        catch (Exception e)
        {
            throw new UnauthorizedException("Authorized user required");
        }
    }

    public async Task<AppUser?> TryGetUserAsync()
    {
        var userId = TryGetUserId();
        var user = await UserManager.FindByIdAsync(userId?.ToString());

        return user;
    }

    public string? GetClaimValue(string claim)
    {
        var claimExists = User.TryGetClaim(claim, out var claimValue);

        return claimExists ? claimValue?.Value : null;
    }
}