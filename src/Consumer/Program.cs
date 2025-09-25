using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consumer.Queues;
using Infrastructure;
using Infrastructure.Database.Repositories;
using Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Interfaces.Repositories;

namespace Consumer;

public class Program
{
    public static async Task Main()
    {
        var builder = new HostBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging();
            })
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterType<LogRepository>()
                                .As<ILogRepository>()
                                .InstancePerLifetimeScope();

                containerBuilder.RegisterType<RabbitMqConnectionProvider>()
                                .AsSelf()
                                .SingleInstance();

                containerBuilder.RegisterType<MotorcycleQueue>()
                                .As<IHostedService>()
                                .SingleInstance();

                containerBuilder.RegisterType<PaymentQueue>()
                                .As<IHostedService>()
                                .SingleInstance();
            });

        await builder.RunConsoleAsync();
    }
}
