namespace EventPlus.Application.Minis.Base;

public interface IMinisHandler<in TRequest, TResult> where TRequest : IMinisRequest<TResult>
{
    Task<TResult> Handle(TRequest request, CancellationToken ct);
}

public interface IMinisHandler<in TRequest> where TRequest: IMinisRequest
{
    Task Handle(TRequest request, CancellationToken ct);
}