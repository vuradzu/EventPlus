using EventPlus.Application.Services.Jwt.Models;
using EventPlus.Domain.Entities.Identity;

namespace EventPlus.Application.Services;

public interface IJwtService
{
    Task<JwtResult> GenerateAsync(AppUser user, long? commandId = null, CancellationToken ct = default);
    Task<JwtResult> RefreshAsync(string refreshToken, long? commandId = null, CancellationToken ct = default);
}