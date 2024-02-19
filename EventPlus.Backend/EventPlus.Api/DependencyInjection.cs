using System.Reflection;
using EventPlus.Api.Extensions;
using EventPlus.Api.Filters;
using EventPlus.Api.Middlewares;
using EventPlus.Application.Jobs.Abstraction;
using EventPlus.Application.Options;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
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
                options.Filters.Add<ArrayCountFilter>();
                options.Filters.Add<SuccessStatusCodesFilter>();
            });

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressInferBindingSourcesForParameters = true; });

        services.AddCustomSwagger();

        services.AddTransient<ExceptionsHandlerMiddleware>();


        return services;
    }

    public static void UseCustomHangfireDashboard(this WebApplication app, IWebHostEnvironment environment)
    {
        var hangfireOptions = app.Configuration.GetSection("Hangfire").Get<HangfireOptions>()!;

        if (environment.IsProduction())
            app.UseHangfireDashboard(hangfireOptions.DashboardUrl, new DashboardOptions
            {
                IsReadOnlyFunc = _ => true,
                Authorization =
                [
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = true,
                        LoginCaseSensitive = true,
                        SslRedirect = true,
                        Users = hangfireOptions.Users
                            .Select(hu => new BasicAuthAuthorizationUser
                            {
                                Login = hu.Username,
                                PasswordClear = hu.Password
                            }).ToArray()
                    })
                ]
            });
        else
            app.UseHangfireDashboard(hangfireOptions.DashboardUrl);
    }
    
    public static void RegisterHangfireJobs()
    {
        var jobTypes = Assembly.GetAssembly(typeof(IJob))!
            .GetTypes()
            .Where(p => p.IsAssignableTo(typeof(IJob)) && p.IsClass);

        foreach (var jobType in jobTypes)
        {
            var job = Activator.CreateInstance(jobType) as IJob;
            job!.Run();
        }
    }
}