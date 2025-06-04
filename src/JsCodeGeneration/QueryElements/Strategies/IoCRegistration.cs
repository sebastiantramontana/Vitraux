using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies;

internal static class IoCRegistration
{
    internal static IServiceCollection AddStrategies(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddAlways()
            .AddOnlyOnceOnDemand();

}