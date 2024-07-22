using EventPlus.Application.Minis.Jwt;
using EventPlus.Application.Minis.Jwt.Authenticate;
using EventPlus.Application.Services.Auth;
using FirebaseAdmin.Auth;
using NeerCore.DependencyInjection;
using NeerCore.Exceptions;

namespace EventPlus.Infrastructure.Services.Auth.ProviderValidators;

[Service]
internal sealed class GoogleAuthProviderValidator : IAuthProviderValidator
{
    public ProviderType Type => ProviderType.Google;

    public async Task<bool> ValidateAsync(JwtAuthenticateRequest request, CancellationToken ct = default)
    {
        request.ProviderMetadata.TryGetValue("token", out var token);

        try
        {
            var firebaseResponse = await FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(token, ct);

            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseResponse.Uid, ct);

            if (request.Email != user.Email)
                throw new ValidationFailedException("Provided wrong email");
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
}