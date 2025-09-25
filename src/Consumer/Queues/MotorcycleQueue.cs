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

public class MotorcycleQueue(
    RabbitMqConnectionProvider connectionProvider,
    ILogRepository _logRepository) : BackgroundService
{
    private readonly RabbitMqConnectionProvider _connectionProvider = connectionProvider;
    private static string QueueName => "motorcycle.created";

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
                var payload = JsonSerializer.Deserialize<MotorcycleDto>(json);

                if (payload != null)
                {
                    logs.Add(Log.New(
                        LogStackLevel.INFO,
                        source: QueueName,
                        action: "MotorcycleReceived",
                        message: $"Moto recebida: {payload.Plate} ({payload.Year})",
                        payload: json
                    ));

                    Console.WriteLine($"[x] Received motorcycle: {payload.Plate} ({payload.Year})");

                    if (payload.Year == 2024)
                    {
                        logs.Add(Log.New(
                            LogStackLevel.INFO,
                            source: $"{QueueName}.2024",
                            action: "MotorcycleCreated2024",
                            message: $"Moto 2024 recebida: {payload.Plate}",
                            payload: json
                        ));
                        Console.WriteLine($"[!] Log 2024 adicionado: {payload.Plate}");
                    }
                }
                else
                {
                    // Só adiciona se o payload for nulo
                    logs.Add(Log.New(
                        LogStackLevel.WARN,
                        source: QueueName,
                        action: "InvalidPayload",
                        message: "Payload inválido recebido",
                        payload: json
                    ));
                }
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
                Console.WriteLine($"[!] Logs para salvar: {logs.Count}");

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
