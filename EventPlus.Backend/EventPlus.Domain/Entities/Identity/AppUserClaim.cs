using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Identity;

public sealed class AppUserClaim : IdentityUserClaim<long>, IEntity
{
    public AppUser? User { get; init; }
}