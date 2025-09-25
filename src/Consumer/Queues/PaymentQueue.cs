using System.Text;
using System.Text.Json;
using Infrastructure.Messaging;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using Shared.Dtos;
using Shared.Enums;
using Shared.Interfaces.Repositories;
using Shared.Models;

namespace Consumer.Queues;

public class PaymentQueue(
    RabbitMqConnectionProvider connectionProvider,
    ILogRepository _logRepository) : BackgroundService
{
    private readonly RabbitMqConnectionProvider _connectionProvider = connectionProvider;
    private static string QueueName => "payment.rental.created";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var connection = await _connectionProvider.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var logs = new List<Log>();

            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var payload = JsonSerializer.Deserialize<PaymentDto>(json);

                if (payload != null)
                {
                    logs.Add(Log.New(
                        LogStackLevel.INFO,
                        source: QueueName,
                        action: "PaymentReceived",
                        message: $"Pagamento recebido para locação, Valor: {payload.TotalAmount}",
                        payload: json
                    ));

                    Console.WriteLine($"[x] Received payment, Amount: {payload.TotalAmount}");

                    if (payload.TotalAmount > 1000)
                    {
                        logs.Add(Log.New(
                            LogStackLevel.INFO,
                            source: $"{QueueName}.HighValue",
                            action: "HighPaymentReceived",
                            message: $"Pagamento alto recebido, Valor: {payload.TotalAmount}",
                            payload: json
                        ));
                    }
                    
                    return;
                }

                logs.Add(Log.New(
                    LogStackLevel.WARN,
                    source: QueueName,
                    action: "InvalidPayload",
                    message: "Payload inválido recebido",
                    payload: json
                ));
            }
            catch (Exception ex)
            {
                logs.Add(Log.New(
                    LogStackLevel.ERROR,
                    source: QueueName,
                    action: "ProcessingError",
                    message: $"Erro ao processar mensagem: {ex.Message}",
                    payload: ex.StackTrace
                ));

                Console.WriteLine($"[!] Erro ao processar mensagem: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"[!] {logs.Count} logs adicionados para PaymentQueue");

                _logRepository.AddRange(logs);
            }
        };

        await channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: true,
            consumerTag: "",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer,
            cancellationToken: stoppingToken
        );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
