namespace Shared.Abstracts;

public abstract class UseCase<TRequest, TData>
{
    public UseCaseResult<TData> Result { get; } = new UseCaseResult<TData>();
    public abstract UseCaseResult<TData> Execute(TRequest request);
}
