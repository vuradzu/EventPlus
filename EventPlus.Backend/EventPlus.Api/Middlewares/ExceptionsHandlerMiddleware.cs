using System.Net;
using EventPlus.Core.Models;
using FluentValidation;
using NeerCore.Api.Extensions;
using NeerCore.Exceptions;

namespace EventPlus.Api.Middlewares;

/// <summary>
///   Exception handling middleware to handle
///   a <see cref="T:NeerCore.Exceptions.HttpException" /> with custom formatted error messages
///   and default 500 exception with fine view otherwise.
/// </summary>
public sealed class ExceptionsHandlerMiddleware : IMiddleware
{
    private readonly ILogger _logger;
    private readonly IHostEnvironment _hostEnvironment;

    /// <inheritdoc cref="T:EventPlus.Api.Middlewares" />
    public ExceptionsHandlerMiddleware(ILoggerFactory loggerFactory, IHostEnvironment hostEnvironment)
    {
        _logger = loggerFactory.CreateLogger(GetType());
        _hostEnvironment = hostEnvironment;
    }

    /// <inheritdoc cref="M:Microsoft.AspNetCore.Http.IMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext,Microsoft.AspNetCore.Http.RequestDelegate)" />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Request {Path} was canceled", context.Request.Path.Value);
        }
        catch (Exception ex)
        {
            await ProcessExceptionAsync(context, ex);
        }
    }

    private async Task ProcessExceptionAsync(HttpContext context, Exception e)
    {
        var logErrorMessage = e switch
        {
            HttpException he => he.StatusCode >= HttpStatusCode.InternalServerError
                ? "Internal Server Error"
                : e.Message,
            ValidationException => "Invalid model received",
            _ => "Unhandled Server Error"
        };
        
        _logger.LogError(e, logErrorMessage);

        var messages = e switch
        {
            ValidationException ve => ve.Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}").ToArray(),
            _ => [e.Message]
        };

        var type = e switch
        {
            HttpException he => he.ErrorType,
            ValidationException => "ValidationFailed",
            _ => "InternalServerError"
        };
        
        var errorResponse = ErrorsResponse.CreateError(
            messages: messages,
            type: type,
            useTrace: _hostEnvironment.IsDevelopment(),
            innerException: e.InnerException);

        var statusCode = e switch
        {
            HttpException he => he.StatusCode,
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.Headers.Append("errors-count", errorResponse.Errors.Count.ToString());
        await context.Response.WriteJsonAsync(statusCode, errorResponse);
    }
}