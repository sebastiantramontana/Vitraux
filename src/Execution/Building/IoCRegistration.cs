using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.Building;

internal static class IoCRegistration
{
    internal static IServiceCollection AddBuilding(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IBuilder, ConfigurationBuilder>()
            .AddSingleton<IVitrauxBuilder, VitrauxBuilder>();
}