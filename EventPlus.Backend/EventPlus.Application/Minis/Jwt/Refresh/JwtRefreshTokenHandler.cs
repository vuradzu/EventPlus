using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services;
using EventPlus.Application.Services.Jwt.Models;

namespace EventPlus.Application.Minis.Jwt.Refresh;

public class JwtRefreshTokenHandler(IJwtService jwtService, IServiceProvider serviceProvider)
    : MinisHandler<JwtRefreshTokenRequest, JwtResult>(serviceProvider)
{
    public override async Task<JwtResult> Handle(JwtRefreshTokenRequest request, CancellationToken ct)
    {
        return await jwtService.RefreshAsync(request.RefreshToken, ct);
    }
}