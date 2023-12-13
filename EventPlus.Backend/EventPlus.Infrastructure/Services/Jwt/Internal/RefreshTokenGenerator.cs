using System.Security.Cryptography;
using EventPlus.Application.Options;
using EventPlus.Application.Services.Jwt.Models;
using EventPlus.Core.Constants;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;

namespace EventPlus.Infrastructure.Services.Jwt.Internal;

[Service]
public sealed class RefreshTokenGenerator(ISqlServerDatabase database, IOptions<JwtOptions> optionsAccessor)
{
    private readonly JwtOptions _options = optionsAccessor.Value;

    public async Task<JwtToken> GenerateAsync(AppUser user, AppDevice device, CancellationToken ct = default)
    {
        var timeCreated = DateTime.UtcNow;
        string token = GenerateRandomToken();

        database.Set<AppToken>().Add(new AppToken
        {
            UserId = user.Id,
            DeviceId = device.Id,
            Name = TokenNames.Refresh,
            LoginProvider = LoginProviders.EventPlus,
            Value = token,
            Created = timeCreated
        });

        await database.SaveChangesAsync(cancel: ct);

        var expires = timeCreated.Add(_options.RefreshToken.Lifetime);
        return new JwtToken(token, expires);
    }

    private static string GenerateRandomToken()
    {
        byte[] randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        // To base64 without ending '=='
        string base64 = Convert.ToBase64String(randomNumber)[..^2];
        return base64.Replace('/', '-').Replace('+', '_');
    }
}