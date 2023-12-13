using EventPlus.Application.Minis.Jwt;
using EventPlus.Application.Minis.Jwt.Authenticate;

namespace EventPlus.Application.Services.Auth;

/// <summary>
/// Specific authorization provider
/// </summary>
public interface IAuthProviderValidator
{
    /// <summary>
    /// Provider type. Can be Telegram and Google
    /// </summary>
    ProviderType Type { get; }

    /// <summary>
    /// Validates authorization data using current provider validator 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default);
}

/// <summary>
/// Single sign-on type
/// </summary>
public enum SsoType
{
    /// <summary>
    /// Register new user
    /// </summary>
    Register,

    /// <summary>
    /// Login
    /// </summary>
    Login
}