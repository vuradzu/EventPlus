using EventPlus.Application.Minis.Base.Mixins;
using EventPlus.Application.Services;
using EventPlus.Domain.Context;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Application.Minis.Base;

/// <summary>
/// Minis handler
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResult">Result type</typeparam>
public abstract class MinisHandler<TRequest, TResult>(IServiceProvider serviceProvider): AutoFluentValidationMixin 
    where TRequest : IMinisRequest<TResult>
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;

    /// <summary>
    /// Database context instance
    /// </summary>
    protected ISqlServerDatabase Database
    {
        get
        {
            _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();

            var userId = UserProvider.TryGetUserId();
            if (userId is not null)
                _database.UserId = userId;

            return _database;
        }
    }

    /// <summary>
    /// User provider instance
    /// </summary>
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    /// <summary>
    /// Main method to process a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    protected abstract Task<TResult> Process(TRequest request, CancellationToken ct);

    public Task<TResult> Handle(TRequest request, CancellationToken ct)
    {
        var validationResult = AutoValidate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
            
        return Process(request, ct);
    }
}

/// <summary>
/// Minis handler without response
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
public abstract class MinisHandler<TRequest>(IServiceProvider serviceProvider): AutoFluentValidationMixin 
    where TRequest : IMinisRequest
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;

    /// <summary>
    /// Database context instance
    /// </summary>
    protected ISqlServerDatabase Database
    {
        get
        {
            _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();

            var userId = UserProvider.TryGetUserId();
            if (userId is not null)
                _database.UserId = userId;

            return _database;
        }
    }
    
    /// <summary>
    /// User provider instance
    /// </summary>
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    /// <summary>
    /// Main method to process a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    protected abstract Task Process(TRequest request, CancellationToken ct);

    public Task Handle(TRequest request, CancellationToken ct)
    {
        var validationResult = AutoValidate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
            
        return Process(request, ct);
    }
}