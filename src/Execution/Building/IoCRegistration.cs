using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.Building.JsViewUpdatingInvokation;

namespace Vitraux.Execution.Building;

internal static class IoCRegistration
{
    internal static IServiceCollection AddBuilding(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IVitrauxBuilder, VitrauxBuilder>()
            .AddSingleton<IJsCreateUpdateFunctionInvoker, JsCreateUpdateFunctionInvoker>()
            .AddSingleton<IJsInitializationInvoker, JsInitializationInvoker>();
}