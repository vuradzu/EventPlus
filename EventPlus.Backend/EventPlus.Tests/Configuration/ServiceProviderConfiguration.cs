using EventPlus.Tests.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Tests.Configuration;

public static class ServiceProviderConfiguration
{
    public static IServiceProvider GetServiceProviderMock() => GetServicesCollectionMock().BuildServiceProvider();

    public static IServiceCollection GetServicesCollectionMock()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AutoInject(UserProviderConfiguration.GetUserProviderMock().Object);

        return serviceCollection;
    }
}