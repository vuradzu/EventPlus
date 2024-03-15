using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Tests.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AutoInject<T>(this IServiceCollection services, T service)
    {
        var implType = typeof(T);

        if (implType.GetInterfaces().Length != 0)
        {
            var interfaceType = implType.GetInterfaces().First();
            services.Add(new ServiceDescriptor(interfaceType, service!));
        }
        else
        {
            services.Add(new ServiceDescriptor(implType, service!));
        }

        return services;
    }
}