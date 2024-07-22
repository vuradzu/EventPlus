namespace EventPlus.Application.Services.Jwt.Models;

public sealed record JwtResult
{
    /// <example>aspadmin</example>
    public required string Username { get; init; }

    public required string FirstName { get; set; }
    public string? LastName { get; set; }

    /// <example>[JWT]</example>
    public required string Token { get; init; }
    public required string RefreshToken { get; init; }

    public DateTimeOffset TokenExpires { get; init; }
    public DateTimeOffset RefreshTokenExpires { get; init; }
    public string? Avatar { get; set; }
    
    public ICollection<long> Commands { get; set; }
}