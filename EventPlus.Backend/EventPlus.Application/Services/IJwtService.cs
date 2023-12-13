using EventPlus.Application.Services.Jwt.Models;
using EventPlus.Domain.Entities.Identity;

namespace EventPlus.Application.Services;

public interface IJwtService
{
    Task<JwtResult> GenerateAsync(AppUser user, CancellationToken ct = default);
    Task<JwtResult> RefreshAsync(string refreshToken, CancellationToken ct = default);
}