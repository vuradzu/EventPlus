using EventPlus.Application.Services;
using EventPlus.Domain.Context;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlus.Application.Minis.Base;

public abstract class MinisHandler<TRequest, TResult>(IServiceProvider serviceProvider)
    where TRequest : IMinisRequest<TResult>
{
    private ISqlServerDatabase? _database;
    private IUserProvider? _userProvider;
    protected ISqlServerDatabase Database => _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();
    protected IUserProvider UserProvider => _userProvider ??= serviceProvider.GetRequiredService<IUserProvider>();

    public abstract Task<TResult> Handle(TRequest request, CancellationToken ct);
}

public abstract class MinisHandler<TRequest>(IServiceProvider serviceProvider)
    where TRequest : IMinisRequest
{
    private ISqlServerDatabase? _database;
    protected ISqlServerDatabase Database => _database ??= serviceProvider.GetRequiredService<ISqlServerDatabase>();

    public abstract Task Handle(TRequest request, CancellationToken ct);
}