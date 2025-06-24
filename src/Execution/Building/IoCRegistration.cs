using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.Building;

internal static class IoCRegistration
{
    internal static IServiceCollection AddBuilding(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IVitrauxBuilder, VitrauxBuilder>();
}