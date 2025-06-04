using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.Building;
using Vitraux.Execution.Building.JsViewUpdatingInvokation;

namespace Vitraux.Execution;

internal static class IoCRegistration
{
    internal static IServiceCollection AddExecution(this IServiceCollection serviceCollection) 
        => serviceCollection
            .AddSingleton<IVitrauxBuilder, VitrauxBuilder>()
            .AddSingleton<IJsInitializationInvoker, JsInitializationInvoker>()
            .AddSingleton<IJsCreateUpdateFunctionInvoker, JsCreateUpdateFunctionInvoker>();
}
