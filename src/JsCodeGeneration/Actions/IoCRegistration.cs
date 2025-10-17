using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.Actions.Parameters;

namespace Vitraux.JsCodeGeneration.Actions;
internal static class IoCRegistration
{
    internal static IServiceCollection AddActions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IActionInputJsElementObjectNamesFilter, ActionInputJsElementObjectNamesFilter>()
            .AddSingleton<IRootActionInputElementsQueryJsGenerator, RootActionInputElementsQueryJsGenerator>()
            .AddSingleton<IRootActionInputEventsRegistrationJsGenerator, RootActionInputEventsRegistrationJsGenerator>()
            .AddSingleton<IRootActionsJsGenerator, RootActionsJsGenerator>()
            .AddSingleton<IRootSingleActionJsGenerator, RootSingleActionJsGenerator>()
            .AddSingleton<IRootSingleParameterlessActionJsGenerator, RootSingleParameterlessActionJsGenerator>()
            .AddSingleton<IStorageElementActionJsLineGenerator, StorageElementActionJsLineGenerator>()
            .AddActionParameters();
}
