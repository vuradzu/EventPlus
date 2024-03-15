using EventPlus.Domain.Entities.Identity;

namespace EventPlus.Application.Services;

public interface IUserProvider
{
    long UserId { get; }
    string UserName { get; }
    long? TryGetUserId();
    Task<AppUser> GetUserAsync();
    Task<AppUser?> TryGetUserAsync();
    string? GetClaimValue(string claim);
}