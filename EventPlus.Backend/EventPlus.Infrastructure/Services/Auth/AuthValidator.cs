using EventPlus.Application.Minis.Jwt.Authenticate;
using EventPlus.Application.Services.Auth;

namespace EventPlus.Infrastructure.Services.Auth;

internal sealed class AuthValidator(IEnumerable<IAuthProviderValidator> validators) : IAuthValidator
{
    public async Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default)
        => await validators.First(v => v.Type == request.Provider).ValidateAsync(request, ct);
}