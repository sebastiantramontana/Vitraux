using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal static class IoCRegistration
{
    internal static IServiceCollection AddJsLineGeneration(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IStorageElementJsLineGeneratorById, StorageElementJsLineGeneratorById>()
            .AddSingleton<IStorageElementJsLineGeneratorByQuerySelector, StorageElementJsLineGeneratorByQuerySelector>()
            .AddSingleton<IStorageElementJsLineGeneratorByTemplate, StorageElementJsLineGeneratorByTemplate>()
            .AddSingleton<IStorageElementJsLineGeneratorByUri, StorageElementJsLineGeneratorByUri>()
            .AddCollections()
            .AddValue();
}