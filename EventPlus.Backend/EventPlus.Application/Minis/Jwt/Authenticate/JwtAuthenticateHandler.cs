using EventPlus.Application.Extensions;
using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services;
using EventPlus.Application.Services.Auth;
using EventPlus.Application.Services.Jwt.Models;
using EventPlus.Core.Extensions;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Jwt.Authenticate;

public class JwtAuthenticateHandler(
    ISqlServerDatabase database,
    UserManager<AppUser> userManager,
    IAuthValidator validator,
    IJwtService jwtService,
    IServiceProvider serviceProvider)
    : MinisHandler<JwtAuthenticateRequest, JwtResult>(serviceProvider)
{
    public override async Task<JwtResult> Handle(JwtAuthenticateRequest request, CancellationToken ct)
    {
        // try to get provider login info
        var loginInfo = await GetUserLoginInfoAsync(request.ProviderKey, request.Provider, ct);

        // if info exists, try to get user
        var user = loginInfo is null ? null : await userManager.FindByIdAsync(loginInfo.UserId.ToString());

        if (user is null)
            // is user is null - register
            user = await RegisterUserAsync(request, ct);
        else
            // else - just validate
            await ValidateUserAsync(request, ct);

        // if validation passed - generate and return tokens
        return await jwtService.GenerateAsync(user, ct);
    }
    
    private async Task<IdentityUserLogin<long>?> GetUserLoginInfoAsync(string key, ProviderType provider, CancellationToken ct)
    {
        string requestLoginProvider = provider.ToString().ToLower();
        return await database.Set<AppUserLogin>()
            .SingleOrDefaultAsync(info =>
                info.ProviderKey == key && info.LoginProvider == requestLoginProvider, ct);
    }
    
    private async Task<AppUser> RegisterUserAsync(JwtAuthenticateRequest request, CancellationToken ct)
    {
        await ValidateUserAsync(request, ct);

        var user = new AppUser
        {
            UserName = Truncate(request.Username.ToLower()),
            FirstName = request.FirstName!,
            LastName = request.LastName,
            Email = request.Email,
            Avatar = request.Avatar,
            EmailConfirmed = request.Provider is not ProviderType.Telegram,
        };

        var result = await userManager.CreateAsync(user);
        if (!result.Succeeded)
            throw new ValidationFailedException("User not created", result.ToErrorDetails());

        await userManager.AddLoginAsync(user,
            new UserLoginInfo(request.Provider.ToString().ToLower(),
                request.ProviderKey,
                request.Provider.ToString().FirstCharToUpper()));

        return user;
    }

    private async Task ValidateUserAsync(JwtAuthenticateRequest request, CancellationToken ct)
    {
        if (request.ProviderMetadata is not { Count: > 0 })
            throw new ValidationFailedException("Metadata not provided");

        bool isValid = await validator.ValidateAsync(request, ct);

        if (!isValid)
            throw new ValidationFailedException("Provided invalid data");
    }

    private string Truncate(string username) => username.Length > 12 ? username[..12] : username;
}