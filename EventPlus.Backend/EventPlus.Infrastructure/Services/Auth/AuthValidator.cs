using EventPlus.Application.Minis.Jwt.Authenticate;
using EventPlus.Application.Services.Auth;
using NeerCore.DependencyInjection;

namespace EventPlus.Infrastructure.Services.Auth;

[Service]
public class AuthValidator(IEnumerable<IAuthProviderValidator> validators) : IAuthValidator
{
    public async Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default)
        => await validators.First(v => v.Type == request.Provider).ValidateAsync(request, ct);
}