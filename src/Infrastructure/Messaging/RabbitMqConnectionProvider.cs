using RabbitMQ.Client;

namespace Infrastructure.Messaging;

public class RabbitMqConnectionProvider
{
    public async Task<IConnection> CreateConnectionAsync()
    {
        var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST")!;
        var user = Environment.GetEnvironmentVariable("RABBITMQ_USER")!;
        var pass = Environment.GetEnvironmentVariable("RABBITMQ_PASS")!;

        var factory = new ConnectionFactory
        {
            HostName = host,
            UserName = user,
            Password = pass,
        };

        return await factory.CreateConnectionAsync();
    }
}
