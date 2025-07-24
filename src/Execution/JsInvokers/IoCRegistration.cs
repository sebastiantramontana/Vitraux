using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.JsInvokers;
internal static class IoCRegistration
{
    internal static IServiceCollection AddInvokers(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsConfigureInvoker, JsConfigureInvoker>()
            .AddSingleton<IJsExecuteUpdateViewFunctionInvoker, JsExecuteUpdateViewFunctionInvoker>()
            .AddSingleton<IJsInitializeNonCachedViewFunctionsInvoker, JsInitializeNonCachedViewFunctionsInvoker>()
            .AddSingleton<IJsTryInitializeViewFunctionsFromCacheByVersionInvoker, JsTryInitializeViewFunctionsFromCacheByVersionInvoker>()
            .AddSingleton<IJsInitializeNewViewFunctionsToCacheByVersionInvoker, JsInitializeNewViewFunctionsToCacheByVersionInvoker>();
}

