using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Identity;

public sealed class AppRole : IdentityRole<long>, IEntity
{
    public override long Id { get; set; }
    public override required string Name { get; set; }
    public override required string NormalizedName { get; set; }
    public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();


    public ICollection<AppUserRole>? UserRoles { get; init; }
    public ICollection<AppRoleClaim>? RoleClaims { get; init; }
}