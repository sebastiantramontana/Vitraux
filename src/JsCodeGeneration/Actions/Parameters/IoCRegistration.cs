using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.Actions.Parameters;
internal static class IoCRegistration
{
    internal static IServiceCollection AddActionParameters(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IActionParameterGettingValueCallGenerator, ActionParameterGettingValueCallGenerator>()
            .AddSingleton<IActionParameterJsElementObjectNamesFilter, ActionParameterJsElementObjectNamesFilter>()
            .AddSingleton<IActionParametersCallbackFunctionNameGenerator, ActionParametersCallbackFunctionNameGenerator>()
            .AddSingleton<IRootActionOnlyOnceAtStartParameterInitQueryJsGenerator, RootActionOnlyOnceAtStartParameterInitQueryJsGenerator>()
            .AddSingleton<IRootActionParametersCallbackJsGenerator, RootActionParametersCallbackJsGenerator>()
            .AddSingleton<IRootActionParametersCallbackArgumentsJsObjectGenerator, RootActionParametersCallbackArgumentsJsObjectGenerator>()
            .AddSingleton<IRootActionParametersCallbackBodyJsGenerator, RootActionParametersCallbackBodyJsGenerator>()
            .AddSingleton<IRootActionParametersCallbackInputValueParameterJsGenerator, RootActionParametersCallbackInputValueParameterJsGenerator>()
            .AddSingleton<IRootActionParametersCallbackQueryElementsJsGenerator, RootActionParametersCallbackQueryElementsJsGenerator>()
            .AddSingleton<IRootActionParametersCallbackConstArgsJsGenerator, RootActionParametersCallbackConstArgsJsGenerator>()
            .AddSingleton<IRootActionParametersCallbackReturnArgsJsGenerator, RootActionParametersCallbackReturnArgsJsGenerator>()
            .AddSingleton<IRootParametrizableActionInputEventsRegistrationJsGenerator, RootParametrizableActionInputEventsRegistrationJsGenerator>()
            .AddSingleton<IRootSingleParametrizableActionJsGenerator, RootSingleParametrizableActionJsGenerator>();
}