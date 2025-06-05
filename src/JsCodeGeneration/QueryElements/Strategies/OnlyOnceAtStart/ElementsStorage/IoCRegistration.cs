using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

internal static class IoCRegistration
{
    internal static IServiceCollection AddElementsStorage(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IOnlyOnceAtStartInitializeJsGenerator, OnlyOnceAtStartInitializeJsGenerator>()
            .AddJsLineGeneration();
}