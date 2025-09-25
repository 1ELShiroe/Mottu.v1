using Application;
using Autofac;
using Infrastructure;

namespace Api.Extensions;

public static class AutofacExtensions
{
    public static void RegisterModules(this ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
              .AsImplementedInterfaces()
              .AsSelf()
              .InstancePerLifetimeScope();

        builder.RegisterModule<ApplicationModule>();
        builder.RegisterModule<InfrastructureModule>();
    }
}
