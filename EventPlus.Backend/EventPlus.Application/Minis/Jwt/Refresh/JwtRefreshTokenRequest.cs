using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services.Jwt.Models;

namespace EventPlus.Application.Minis.Jwt.Refresh;

public class JwtRefreshTokenRequest: IMinisRequest<JwtResult>
{
    public required string RefreshToken { get; set; }
}