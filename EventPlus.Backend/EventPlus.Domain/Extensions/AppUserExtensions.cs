using EventPlus.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.Domain.Extensions;

public static class AppUserExtensions
{
    public static async Task<AppUser?> GetByLoginAsync(this IQueryable<AppUser> dbUsers, string login, CancellationToken ct = default)
    {
        login = login.ToUpperInvariant();
        return await dbUsers
            .Where(e => e.NormalizedUserName == login || e.NormalizedEmail == login)
            .FirstOrDefaultAsync(ct);
    }
}