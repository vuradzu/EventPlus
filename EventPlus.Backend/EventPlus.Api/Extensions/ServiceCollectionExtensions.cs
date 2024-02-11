using EventPlus.Api.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EventPlus.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.Configure<SwaggerGenOptions>(options =>
        {
            // Document Filters
            // options.DocumentFilter<ResponsesFilter>();
            // Operation Filters
            // options.OperationFilter<FormContentTypeSchemaOperationFilter>();
            // Schema Filters
            // options.SchemaFilter<MultiSourceFilter>();
            options.OperationFilter<SwaggerAddCommandHeaderFilter>();
            options.EnableAnnotations();
            options.SupportNonNullableReferenceTypes();
        });
    }
}