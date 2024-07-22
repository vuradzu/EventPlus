using EventPlus.Api.Filters.Requirements;
using EventPlus.Core.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EventPlus.Api.Filters;

public class SwaggerAddCommandHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = HeaderConstants.CommandIdHeaderName,
            In = ParameterLocation.Header,
            Description = "Current command Id",
            Required = false
        });
    }
}