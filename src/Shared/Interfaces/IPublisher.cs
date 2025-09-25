namespace Shared.Interfaces;

public interface IPublisher<T>
{
    Task PublishAsync(T objectFile);
}
