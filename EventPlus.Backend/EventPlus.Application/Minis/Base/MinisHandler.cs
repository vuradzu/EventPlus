using EventPlus.Application.Services;
using EventPlus.Domain.Context;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Application.Minis.Base;

/// <summary>
/// Minis handler
/// </summary>
/// <param name="serviceProvider"></param>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResult">Result type</typeparam>
public abstract class MinisHandler<TRequest, TResult>(IServiceProvider serviceProvider)
    where TRequest : IMinisRequest<TResult>
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    
    /// <summary>
    /// Database context instance
    /// </summary>
    protected ISqlServerDatabase Database => _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();
    
    /// <summary>
    /// User provider instance
    /// </summary>
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    /// <summary>
    /// Main method to handle a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    public abstract Task<TResult> Handle(TRequest request, CancellationToken ct);
}

/// <summary>
/// Minis handler without response
/// </summary>
/// <param name="serviceProvider"></param>
/// <typeparam name="TRequest">Request type</typeparam>
public abstract class MinisHandler<TRequest>(IServiceProvider serviceProvider)
    where TRequest : IMinisRequest
{
    private ISqlServerDatabase? _database;
    
    /// <summary>
    /// Database context instance
    /// </summary>
    protected ISqlServerDatabase Database => _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();
    private IUserProvider? _userProvider;
    
    /// <summary>
    /// User provider instance
    /// </summary>
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    /// <summary>
    /// Main method to handle a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="ct">Cancellation token</param>
    public abstract Task Handle(TRequest request, CancellationToken ct);
}