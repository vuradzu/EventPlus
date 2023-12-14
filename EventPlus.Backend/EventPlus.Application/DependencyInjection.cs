using System.Reflection;
using EventPlus.Application.Minis.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinis();

        return services;
    }

    private static IServiceCollection AddMinis(this IServiceCollection services)
    {
        Type[] minisTypes = { typeof(MinisHandler<>), typeof(MinisHandler<,>) };

        var assembly = Assembly.GetAssembly(typeof(MinisHandler<>))!;
        var exportedTypes = assembly.GetTypes();

        var types = exportedTypes
            .Where(type => type.BaseType is { IsGenericType: true }
                           && minisTypes.Any(minisType => type.BaseType.GetGenericTypeDefinition() == minisType));

        foreach (var type in types)
            services.AddTransient(type, type);

        return services;
    }
}