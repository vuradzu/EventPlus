using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventPlus.Application.Options;
using EventPlus.Application.Services.Jwt.Models;
using EventPlus.Core.Constants;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities;
using EventPlus.Domain.Entities.Authorization;
using EventPlus.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeerCore.DependencyInjection;

namespace EventPlus.Infrastructure.Services.Jwt.Internal;

[Service]
public sealed class AccessTokenGenerator(IOptions<JwtOptions> optionsAccessor, ISqlServerDatabase database)
{
    private readonly JwtOptions.AccessTokenOptions _options = optionsAccessor.Value.AccessToken;

    public async Task<JwtToken> GenerateAsync(AppUser user, long? commandId = null, CancellationToken ct = default)
    {
        var expires = DateTime.UtcNow.Add(_options.Lifetime);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(await GetUserClaimsAsync(user, commandId, ct)),
            SigningCredentials = new SigningCredentials(_options.Secret, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _options.Issuer,
            Audience = _options.Audiences is { Length: > 0 } ? _options.Audiences[0] : null,
            IssuedAt = DateTime.UtcNow,
            Expires = expires,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken jwt = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtToken(tokenHandler.WriteToken(jwt), expires);
    }

    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(AppUser user, long? commandId = null, CancellationToken ct = default)
    {
        var claims = new List<Claim>
        {
            new(Claims.Id, user.Id.ToString()),
            new(Claims.Username, user.UserName),
            new(Claims.Avatar, user.Avatar ?? ""),
            new(Claims.FirstName, user.FirstName),
        };

        if (commandId is not null)
        {
            var (roles, rolesPermissions) = await GetUserRolesInCommand(user.Id, commandId.Value, ct);
            var permissions = rolesPermissions.Union(await GetUserPermissionsInCommand(user.Id, commandId.Value, ct));
            
            claims.AddRange(roles.Select(role => new Claim(Claims.Role, role)));
            claims.AddRange(permissions.Select(permission => new Claim(Claims.Permissions, permission)));
        }
        
        return claims;
    }

    private async Task<(string[], string[])> GetUserRolesInCommand(long userId, long commandId, CancellationToken ct)
    {
        var roles = await database.Set<CommandMemberRole>()
            .Include(cmr => cmr.Role)
            .ThenInclude(cr => cr!.RolePermissions)!
            .ThenInclude(rp => rp.Permission)
            .Include(cmr => cmr.Member)
            .Where(cmr => cmr.Member!.AppUserId == userId)
            .Where(cmr => cmr.Member!.CommandId == commandId)
            .Select(cmr => cmr.Role!)
            .ToArrayAsync(ct);

        var rolesPermissions = roles
            .SelectMany(r => r.RolePermissions!)
            .Select(rp => rp.Permission!.Title)
            .ToArray();

        return (roles.Select(r => r.Title).ToArray(), rolesPermissions);
    }

    private Task<string[]> GetUserPermissionsInCommand(long userId, long commandId, CancellationToken ct)
    {
        var permissions = database.Set<CommandMemberPermission>()
            .Include(cmp => cmp.Permission)
            .Include(cmp => cmp.Member)
            .Where(cmp => cmp.Member!.AppUserId == userId)
            .Where(cmp => cmp.Member!.CommandId == commandId)
            .Select(cmp => cmp.Permission!.Title)
            .ToArrayAsync(ct);

        return permissions;
    }
}