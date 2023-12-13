using EventPlus.Domain.Context;
using EventPlus.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseWithUserIdentity();
        
        return services;
    } 
    
    private static void AddDatabaseWithUserIdentity(this IServiceCollection services)
    {
        var contextFactory = new SqlServerDbContextFactory();
        services.AddDbContext<SqlServerDbContext>(cob => contextFactory.ConfigureContextOptions(cob));

        services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SqlServerDbContext>()
            .AddUserManager<UserManager<AppUser>>();

        services.AddScoped<ISqlServerDatabase, SqlServerDbContext>();
    }
}