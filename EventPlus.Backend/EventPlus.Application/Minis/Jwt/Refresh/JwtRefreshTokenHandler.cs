using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services;
using EventPlus.Application.Services.Jwt.Models;

namespace EventPlus.Application.Minis.Jwt.Refresh;

public class JwtRefreshTokenHandler(IJwtService jwtService, IServiceProvider serviceProvider)
    : MinisHandler<JwtRefreshTokenRequest, JwtResult>(serviceProvider)
{
    protected override async Task<JwtResult> Process(JwtRefreshTokenRequest request, CancellationToken ct)
    {
        return await jwtService.RefreshAsync(request.RefreshToken, request.CommandId, ct);
    }
}