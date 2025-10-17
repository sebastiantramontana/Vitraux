using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.JsInvokers.ViewFunctions;
internal static class IoCRegistration
{
    internal static IServiceCollection AddViewFunctions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsExecuteUpdateViewFunctionInvoker, JsExecuteUpdateViewFunctionInvoker>()
            .AddSingleton<IJsInitializeNonCachedViewFunctionsInvoker, JsInitializeNonCachedViewFunctionsInvoker>()
            .AddSingleton<IJsTryInitializeViewFunctionsFromCacheByVersionInvoker, JsTryInitializeViewFunctionsFromCacheByVersionInvoker>()
            .AddSingleton<IJsInitializeNewViewFunctionsToCacheByVersionInvoker, JsInitializeNewViewFunctionsToCacheByVersionInvoker>();
}

