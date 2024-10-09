using EventPlus.Application.Minis.Base.Mixins;
using EventPlus.Application.Services;
using EventPlus.Core.Constants;
using EventPlus.Domain.Context;
using EventPlus.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Application.Minis.Base;

/// <summary>
/// Minis handler
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResult">Result type</typeparam>
public abstract class MinisHandler<TRequest, TResult>(IServiceProvider serviceProvider) : AutoFluentValidationMixin
    where TRequest : IMinisRequest<TResult>
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    private IHttpContextAccessor? _httpContextAccessor;

    /// <summary>
    /// Database context instance
    /// </summary>
    public ISqlServerDatabase Database
    {
        get
        {
            _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();

            var userId = UserProvider.TryGetUserId();
            if (userId is not null)
                _database.UserId = userId;

            return _database;
        }
        set => _database = value;
    }

    /// <summary>
    /// User provider instance
    /// </summary>
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    /// <summary>
    /// Http context accessor instance
    /// </summary>
    protected IHttpContextAccessor HttpContextAccessor =>
        _httpContextAccessor ??= serviceProvider.GetRequiredService<IHttpContextAccessor>();

    /// <summary>
    /// Custom business logic
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Instance of the TResult type</returns>
    protected abstract Task<TResult> Process(TRequest request, CancellationToken ct);

    /// <summary>
    /// Main method to validate and handle a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Instance of the TResult type</returns>
    /// <exception cref="ValidationException">If validation doesn't pass, ValidationException will be thrown</exception>
    public async Task<TResult> Handle(TRequest request, CancellationToken ct)
    {
        var validationResult = AutoValidate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var processResult = await Process(request, ct);
        await UpdateCommandLastActivity();

        return processResult;
    }

    private async Task UpdateCommandLastActivity()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        var commandIdHeaderExists =
            httpContext!.Request.Headers.TryGetValue(HeaderConstants.CommandIdHeaderName,
                out var commandIdFromHeader);

        if (!commandIdHeaderExists) return;

        var command = await Database.Set<Command>().FirstOrDefaultAsync(c => c.Id == commandIdFromHeader);

        if (command is null) return;

        command.LastActivity = DateTime.UtcNow;

        await Database.SaveChangesAsync();
    }
}

/// <summary>
/// Minis handler without response
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
public abstract class MinisHandler<TRequest>(IServiceProvider serviceProvider) : AutoFluentValidationMixin
    where TRequest : IMinisRequest
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    private IHttpContextAccessor? _httpContextAccessor;

    /// <summary>
    /// Database context instance
    /// </summary>
    public ISqlServerDatabase Database
    {
        get
        {
            _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();

            var userId = UserProvider.TryGetUserId();
            if (userId is not null)
                _database.UserId = userId;

            return _database;
        }
        set => _database = value;
    }

    /// <summary>
    /// User provider instance
    /// </summary>
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    /// <summary>
    /// Http context accessor instance
    /// </summary>
    protected IHttpContextAccessor HttpContextAccessor =>
        _httpContextAccessor ??= serviceProvider.GetRequiredService<IHttpContextAccessor>();

    /// <summary>
    /// Custom business logic
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    protected abstract Task Process(TRequest request, CancellationToken ct);


    /// <summary>
    /// Main method to validate and handle a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    /// <exception cref="ValidationException">If validation doesn't pass, ValidationException will be thrown</exception>
    public async Task Handle(TRequest request, CancellationToken ct)
    {
        var validationResult = AutoValidate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await Process(request, ct);
        await UpdateCommandLastActivity();
    }

    private async Task UpdateCommandLastActivity()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        var commandIdHeaderExists =
            httpContext!.Request.Headers.TryGetValue(HeaderConstants.CommandIdHeaderName,
                out var commandIdFromHeader);

        if (!commandIdHeaderExists) return;

        var command = await Database.Set<Command>().FirstOrDefaultAsync(c => c.Id == commandIdFromHeader);

        if (command is null) return;

        command.LastActivity = DateTime.UtcNow;

        await Database.SaveChangesAsync();
    }
}