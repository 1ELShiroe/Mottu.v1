using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.Abstracts;
using Shared.Dtos;
using Shared.Interfaces;

namespace Infrastructure.Messaging.Publishers;

public class PaymentPublisher(
    RabbitMqConnectionProvider _connectionProvider) : Publisher<PaymentDto>, IPublisher<PaymentDto>
{
    protected override string ExchangeName => "payment_exchange";
    protected override string QueueName => "payment.rental.created";

    public override async Task PublishAsync(PaymentDto message)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync();

        using var channel = await connection.CreateChannelAsync();
        var basicProperties = new BasicProperties
        {
            ContentType = "application/json",
            DeliveryMode = DeliveryModes.Persistent
        };

        await channel.ExchangeDeclareAsync(exchange: ExchangeName, type: ExchangeType.Direct, durable: true);
        await channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false);

        await channel.QueueBindAsync(queue: QueueName, exchange: ExchangeName, routingKey: QueueName);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: QueueName,
            mandatory: false,
            basicProperties: basicProperties,
            body: body
        );
    }
}
