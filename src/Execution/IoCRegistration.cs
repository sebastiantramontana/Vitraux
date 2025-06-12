using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.Building;
using Vitraux.Execution.Building.JsViewUpdatingInvokation;
using Vitraux.Execution.Serialization;

namespace Vitraux.Execution;

internal static class IoCRegistration
{
    internal static IServiceCollection AddExecution(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsInitializationInvoker, JsInitializationInvoker>()
            .AddSingleton<IJsCreateUpdateFunctionInvoker, JsCreateUpdateFunctionInvoker>()
            .AddSingleton<IJsIsVersionedUpdateViewFunctionRebuildNeededInvoker, JsIsVersionedUpdateViewFunctionRebuildNeededInvoker>()
            .AddSingleton<IJsExecuteUpdateViewFunctionInvoker, JsExecuteUpdateViewFunctionInvoker>()
            .AddSerialization()
            .AddBuilding();

}
