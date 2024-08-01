namespace EventPlus.Application.Services.Jwt.Models;

/// <summary>
/// Wrapper for jwt token and expires token time.
/// </summary>
/// <param name="Token">Jwt token</param>
/// <param name="Expires">Expires time</param>
public record struct JwtToken(string Token, DateTime Expires);