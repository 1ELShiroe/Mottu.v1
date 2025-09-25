using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Application;

[ExcludeFromCodeCoverage]
public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var appAssembly = typeof(ApplicationException).Assembly;

        builder.RegisterAssemblyTypes(appAssembly)
               .AsImplementedInterfaces()
               .AsSelf()
               .InstancePerLifetimeScope();
    }
}