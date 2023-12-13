namespace EventPlus.Application.Services.Jwt.Models;

public sealed class RefreshTokenDto
{
    public string Value { get; set; } = default!;
    public DateTimeOffset ExpirationTime { get; set; }
}