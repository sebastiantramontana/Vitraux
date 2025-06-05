using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal static class IoCRegistration
{
    internal static IServiceCollection AddCollections(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IStorageElementCollectionJsLineGenerator, StorageElementCollectionJsLineGenerator>()
            .AddSingleton<IStorageElementCollectionJsLineGeneratorByTemplate, StorageElementCollectionJsLineGeneratorByTemplate>()
            .AddSingleton<IStorageElementCollectionJsLineGeneratorByUri, StorageElementCollectionJsLineGeneratorByUri>();
}