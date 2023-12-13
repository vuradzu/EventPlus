using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Identity;

public sealed class AppUser : IdentityUser<long>, IEntity
{
    public override long Id { get; set; }
    public override required string UserName { get; set; }

    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public override string NormalizedUserName { get; set; } = default!;
    public override string? Email { get; set; }
    public override string? NormalizedEmail { get; set; }

    public string? Description { get; set; }
    public string? Avatar { get; set; }

    public DateTimeOffset Registered { get; set; } = DateTimeOffset.UtcNow;


    public ICollection<AppUserRole>? UserRoles { get; init; }
    public ICollection<AppUserClaim>? UserClaims { get; init; }
    public ICollection<AppToken>? Tokens { get; init; }
}