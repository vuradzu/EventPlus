using EventPlus.Api.Extensions;
using EventPlus.Api.Filters;
using EventPlus.Application.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NeerCore.Api.Extensions;
using NeerCore.DependencyInjection.Extensions;
using ExceptionHandlerOptions = NeerCore.Api.ExceptionHandlerOptions;

namespace EventPlus.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.Configure<ExceptionHandlerOptions>(o =>
            o.Extended500ExceptionMessage = environment.IsDevelopment());

        services.AddHttpClient();
        services.ConfigureAllOptions();

        services.AddJwtAuthentication();
        services.AddPoliciesAuthorization();

        services.AddNeerApiServices();
        services.AddAllServices(o => o.ResolveInternalImplementations = true);
        services.AddNeerControllers()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<UnifiedResponseFilter>();
                options.Filters.Add<SuccessStatusCodesFilter>();
            });

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressInferBindingSourcesForParameters = true; });

        services.AddCustomSwagger();


        return services;
    }
}