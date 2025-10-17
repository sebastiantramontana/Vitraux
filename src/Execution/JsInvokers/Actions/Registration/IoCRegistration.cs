using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.JsInvokers.ViewFunctions;

namespace Vitraux.Execution.JsInvokers.Actions.Registration;

internal static class IoCRegistration
{
    internal static IServiceCollection AddActionRegistration(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsExecuteActionRegistrationsFunctionInvoker, JsExecuteActionRegistrationsFunctionInvoker>()
            .AddSingleton<IJsInitializeNewActionsFunctionToCacheByVersionInvoker, JsInitializeNewActionsFunctionToCacheByVersionInvoker>()
            .AddSingleton<IJsInitializeNonCachedActionsFunctionInvoker, JsInitializeNonCachedActionsFunctionInvoker>()
            .AddSingleton<IJsTryInitializeActionsFunctionFromCacheByVersionInvoker, JsTryInitializeActionsFunctionFromCacheByVersionInvoker>();
}

