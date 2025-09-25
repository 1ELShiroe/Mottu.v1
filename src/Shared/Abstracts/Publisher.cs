namespace Shared.Abstracts;

public abstract class Publisher<T>
{
    protected abstract string ExchangeName { get; }
    protected abstract string QueueName { get; }
    public abstract Task PublishAsync(T objectFile);
}