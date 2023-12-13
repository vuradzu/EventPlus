using Microsoft.AspNetCore.Identity;
using NeerCore.Data.Abstractions;

namespace EventPlus.Domain.Entities.Identity;

public sealed class AppUserLogin : IdentityUserLogin<long>, IEntity { }