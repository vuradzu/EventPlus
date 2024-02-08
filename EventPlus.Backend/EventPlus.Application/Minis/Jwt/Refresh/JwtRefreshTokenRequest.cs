using EventPlus.Application.Minis.Base;
using EventPlus.Application.Services.Jwt.Models;
using FluentValidation;

namespace EventPlus.Application.Minis.Jwt.Refresh;

public class JwtRefreshTokenRequest : IMinisRequest<JwtResult>
{
    public required string RefreshToken { get; set; }
    public long? CommandId { get; set; }
}

public class JwtRefreshTokenValidator : AbstractValidator<JwtRefreshTokenRequest>
{
    public JwtRefreshTokenValidator()
    {
        RuleFor(rt => rt.RefreshToken).NotNull().NotEmpty().WithMessage("Wrong refresh token");
    }
}