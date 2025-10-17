using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.Actions;
using Vitraux.Execution.Building;
using Vitraux.Execution.JsInvokers;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution;

internal static class IoCRegistration
{
    internal static IServiceCollection AddExecution(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddActions()
            .AddSerialization()
            .AddBuilding()
            .AddInvokers()
            .AddViewModelNames();
}
