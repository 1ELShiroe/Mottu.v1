using System.Diagnostics.CodeAnalysis;
using Autofac;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Publishers;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Entities;
using Shared.Interfaces;

namespace Infrastructure;

[ExcludeFromCodeCoverage]
public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

        var connection = Environment.GetEnvironmentVariable("DBCONN_POSTGRES");

        builder.RegisterType<RabbitMqConnectionProvider>().InstancePerLifetimeScope();

        builder.RegisterType<MotorcyclePublisher>().InstancePerLifetimeScope();
        builder.RegisterType<PaymentPublisher>().InstancePerLifetimeScope();

        builder.RegisterType<MotorcyclePublisher>()
               .As<IPublisher<MotorcycleDto>>()
               .InstancePerLifetimeScope();

        builder.RegisterType<PaymentPublisher>()
                .As<IPublisher<PaymentDto>>()
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
            .Where(t => (t.Namespace ?? string.Empty).Contains("Database"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterBuildCallback(scope =>
        {
            var context = scope.Resolve<Database.Context>();

            if (!string.IsNullOrEmpty(connection))
            {
                context.Database.Migrate();

                if (!context.RentalPlans.Any())
                {
                    context.RentalPlans.AddRange(
                        new RentalPlan { Id = Guid.CreateVersion7(), DurationInDays = 7, DailyCost = 30 },
                        new RentalPlan { Id = Guid.CreateVersion7(), DurationInDays = 15, DailyCost = 28 },
                        new RentalPlan { Id = Guid.CreateVersion7(), DurationInDays = 30, DailyCost = 22 },
                        new RentalPlan { Id = Guid.CreateVersion7(), DurationInDays = 45, DailyCost = 20 },
                        new RentalPlan { Id = Guid.CreateVersion7(), DurationInDays = 50, DailyCost = 18 }
                    );

                    context.SaveChanges();
                }
                return;
            }

            context.Database.EnsureCreated();
        });
    }
}