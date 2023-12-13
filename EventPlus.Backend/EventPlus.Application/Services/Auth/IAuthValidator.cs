using EventPlus.Application.Minis.Jwt.Authenticate;

namespace EventPlus.Application.Services.Auth;

/// <summary>
/// Authorization validator. Searches specific provider based on request.Provider enum, and validates the authorization data.
/// </summary>
public interface IAuthValidator
{
    /// <summary>
    /// Searches specific provider based on request.Provider enum, and validates the authorization data.
    /// </summary>
    /// <param name="request">Provider data</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default);
}